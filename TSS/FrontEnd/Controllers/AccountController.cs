using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private SecurityHelper _securityHelper = new();
        private GenresHelper _genresHelper = new();
        private IdentificationsHelper _identificationsHelper = new();
        private ProvincesHelper _provincesHelper = new();

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
                TokenModel token = _securityHelper.Login(model);
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
        public async Task<ActionResult> Register(UserViewModel user)
        {
            var gRecaptchaResponse = Request.Form["g-recaptcha-response"];
            if (await IsReCaptchaPassedAsync(gRecaptchaResponse) && ModelState.IsValid)
            {
                user = _securityHelper.Register(user);
                TempData["Message"] = "Usuario creado correctamente.";
                return RedirectToAction("Login");
            }
            else
            {
                //catch (Exception ex)
                //{              
                TempData["Error"] = "No se ha creado el usuario.";
                return RedirectToAction("Register");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        public async Task<bool> IsReCaptchaPassedAsync(string gRecaptchaResponse)
        {
            if (string.IsNullOrEmpty(gRecaptchaResponse))
            {
                return false;
            }

            using (var httpClient = new HttpClient())
            {
                var secretKey = _configuration["RecaptchaSettings:SecretKey"];
                var googleVerificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={gRecaptchaResponse}";

                var httpResponseMessage = await httpClient.GetAsync(googleVerificationUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(jsonResponse);

                    return reCaptchaResponse.Success;
                }
                else
                {
                    return false;
                }
            }
        }

        public class ReCaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }
    }
}






//try
//{
//    if (model != null)
//    {

//        TokenModel tokenModel = _seguridadHelper.Login(model);
//        HttpContext.Session.SetString("token", tokenModel.Token);
//        var EsValido = false;

//        if (tokenModel != null)
//        {
//            EsValido = true;

//        }
//        if (!EsValido)
//        {
//            ViewBag.Message = "Invalid Credentials";
//            return View(model);
//        }
//        var loginModel = _seguridadHelper.GetUser(model);
//        var claims = new List<Claim>() {
//                                 new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
//                                 new Claim(ClaimTypes.Name, loginModel.Email)
//                    };

//        foreach (var item in loginModel.Roles)
//        {
//            claims.Add(
//                  new Claim(ClaimTypes.Role, item.ToString())
//                );
//        }

//        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//        var principal = new ClaimsPrincipal(identity);
//        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()

//        {
//            IsPersistent = model.RememberLogin
//        });
//        return LocalRedirect(model.ReturnUrl);
//    }
//    return View(model);
//}
//catch (Exception)
//{
//    return View("AccessDenied");
//}