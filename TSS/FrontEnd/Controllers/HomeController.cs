using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        UsersHelper Helper = new();
        GenresHelper genresHelper = new();
        IdentificationsHelper idHelper = new();
        ProvincesHelper provincesHelper = new();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            UserViewModel user = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel user)
        {
            try
            {
                user = Helper.Add(user);
                TempData["Success"] = "¡Usuario registrado correctamente!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Datos inválidos, intente de nuevo. " + ex.Message;
                return View("Register");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}