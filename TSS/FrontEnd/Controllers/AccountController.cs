using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private AppointmentsHelper _appointmentsHelper = new();
        private RecordsHelper _recordsHelper = new();
        private SecurityHelper _securityHelper = new();
        private GenresHelper _genresHelper = new();
        private IdentificationsHelper _identificationsHelper = new();
        private ProvincesHelper _provincesHelper = new();
        private UsersHelper usersHelper = new();

        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Login(string Url = "/")
        {
            LoginModel model = new()
            {
                ReturnUrl = Url
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                TokenModel? token = _securityHelper.Login(model);
                HttpContext.Session.SetString("token", token!.Token);

                var loginModel = _securityHelper.GetUser(model);
                loginModel.Roles = _securityHelper.GetRole(model);
                var claims = new List<Claim>() {
                    new(ClaimTypes.NameIdentifier, loginModel.IdNumber),
                    new(ClaimTypes.Name, loginModel.UserName),
                    new(ClaimTypes.Surname, loginModel.FirstName),
                    new(ClaimTypes.GivenName, loginModel.LastName),
                    new(ClaimTypes.Email, loginModel.Email),
                    new(ClaimTypes.Role, loginModel.Roles)
                 };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()

                {
                    IsPersistent = model.RememberLogin
                });

                if (loginModel.Roles == "Admin")
                {
                    TempData["Message"] = $"Bienvenido/a {loginModel.UserName} {loginModel.FirstName}.";
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (loginModel.Roles == "User")
                {
                    TempData["Message"] = $"Bienvenido/a {loginModel.UserName} {loginModel.FirstName}.";
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["Error"] = "Datos inválidos, intente de nuevo.";
                return RedirectToAction("Login");
            }
        }

        public IActionResult Register()
        {
            UserViewModel user = new();
            var genres = _genresHelper.GetAllView();
            var ids = _identificationsHelper.GetAllView();
            var provinces = _provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");

            string SiteKey = _configuration["RecaptchaSettings:SiteKey"];
            ViewData["Key"] = SiteKey;
            return View(user);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel? user)
        {
            var gRecaptchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = _configuration["RecaptchaSettings:SecretKey"];
            bool response = await IsReCaptchaPassedAsync(gRecaptchaResponse, secretKey);

            string mensaje = _securityHelper.Register(user);
            if (response && ModelState.IsValid && mensaje.StartsWith("U"))
            {
                //TempData["Message"] = "Usuario creado correctamente.";
                TempData["Message"] = mensaje;
                return RedirectToAction("Login");
            }
            else
            {
                //TempData["Error"] = "No se ha creado el usuario.";
                TempData["Error"] = mensaje;
                return RedirectToAction("Register");
            }
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        #region ReCaptcha
        public static async Task<bool> IsReCaptchaPassedAsync(string gRecaptchaResponse, string secretKey)
        {
            if (string.IsNullOrEmpty(gRecaptchaResponse))
            {
                return false;
            }

            using var httpClient = new HttpClient();
            var googleVerificationUrl = $"https://www.google.com/recaptcha/api/siteverify";
            var postData = new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("secret", secretKey),
              new KeyValuePair<string, string>("response", gRecaptchaResponse)
            };

            var httpResponseMessage = await httpClient.PostAsync(googleVerificationUrl, new FormUrlEncodedContent(postData));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(jsonResponse);

                if (reCaptchaResponse == null)
                {
                    return false;
                }

                return reCaptchaResponse.Success;
            }
            else
            {
                return false;
            }
        }

        private class ReCaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; } = false;
        }
        #endregion

        #region UserActions
        [Authorize]
        public ActionResult MyAppointments()
        {
            try
            {
                string? email = User.FindFirst(ClaimTypes.Email)?.Value;
                List<VWAdminAppointmentViewModel>? model = _appointmentsHelper.GetUserAppointments(email!);
                return View(model);

            }
            catch (Exception)
            {
                TempData["Error"] = "No tienes citas disponibles.";
                return View();
            }
        }

        [Authorize]
        public ActionResult MyRecords()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            List<VWRecordViewModel> model = _recordsHelper.GetUserRecords(email!);
            return View(model);
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            UserViewModel? user = _securityHelper.GetEmail(email!);
            var genres = _genresHelper.GetAllView();
            var ids = _identificationsHelper.GetAllView();
            var provinces = _provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserViewModel user)
        {
            string mensaje = usersHelper.Edit(user);

            if (mensaje.StartsWith("U"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("EditProfile", "Account");
            }
            else
            {
                TempData["Error"] = "Perfil no editado.";
                return RedirectToAction("EditProfile", "Account");
            }
        }

        public IActionResult ResetPassword()
        {
            EmailModel model = new();
            return View(model);
        }

        //[AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(EmailModel user)
        {
            if (user.To != null)
            {
                string message = _securityHelper.ForgotPassword(user);
                TempData["Message"] = message;
                return RedirectToAction("ResetPassword");
            }
            else
            {
                TempData["Error"] = "Correo electrónico inválido.";
                return RedirectToAction("ResetPassword");
            }
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            PasswordModel model = new();
            return View(model);
        }

        [Authorize]
        //[AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(PasswordModel model)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            model.Email = email!;
            string message = _securityHelper.ChangePassword(model);
            if (message.StartsWith("Se"))
            {
                TempData["Message"] = message;
                return RedirectToAction("ChangePassword");
            }
            else
            {
                TempData["Error"] = message;
                return RedirectToAction("ChangePassword");
            }
        }

        #endregion
    }
}
