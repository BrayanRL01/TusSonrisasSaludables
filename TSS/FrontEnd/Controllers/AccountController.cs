using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Reflection;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        SecurityHelper securityHelper;
        public AccountController()
        {
        }

        public IActionResult Login(string returnUrl = "/")
        {
            LoginModel model = new();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                TokenModel token = securityHelper.Login(model);
                HttpContext.Session.SetString("token", token.Token);
                var loginModel = securityHelper.GetUser(model);
                var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
                        new Claim(ClaimTypes.Name, loginModel.Email)
                 };

                foreach (var item in loginModel.Roles)
                {
                    claims.Add(
                          new Claim(ClaimTypes.Role, item.ToString())
                        );
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()

                {
                    IsPersistent = model.RememberLogin
                });
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(model);
            }

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
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