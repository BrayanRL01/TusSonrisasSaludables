using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using AspNetCore.ReCaptcha;

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
                HttpContext.Session.SetString("token", token.Token);

                var loginModel = _securityHelper.GetUser(model);
                loginModel.Roles = _securityHelper.GetRole(model);
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.IdNumber),
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Surname, loginModel.FirstName),
                    new Claim(ClaimTypes.GivenName, loginModel.LastName),
                    new Claim(ClaimTypes.Email, loginModel.Email),
                    new Claim(ClaimTypes.Role, loginModel.Roles)
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
                    return RedirectToAction("Index", "Dashboard", model);
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

        public ActionResult Register()
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
        public async Task<ActionResult> Register(UserViewModel? user)
        {
            var gRecaptchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = _configuration["RecaptchaSettings:SecretKey"];
            bool response = await IsReCaptchaPassedAsync(gRecaptchaResponse, secretKey);
            if (response && ModelState.IsValid)
            {
                user = _securityHelper.Register(user);
                TempData["Message"] = "Usuario creado correctamente.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Error"] = "No se ha creado el usuario.";
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
            var googleVerificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={gRecaptchaResponse}";

            var httpResponseMessage = await httpClient.GetAsync(googleVerificationUrl);

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
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            List<VWAdminAppointmentViewModel> model = _appointmentsHelper.GetUserAppointments(email!);
            return View(model);
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
            UserViewModel user = _securityHelper.GetEmail(email!);
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
        public ActionResult EditProfile([FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                user = usersHelper.Edit(user);
                TempData["Message"] = "Perfil editado correctamente.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Perfil no editado.";
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

    }
}
