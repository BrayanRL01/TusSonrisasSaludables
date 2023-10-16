using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Drawing;

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
            //var captchaCode = GenerateCaptchaCode();
            //var image = GenerateCaptchaImage(captchaCode);

            //using (var stream = new MemoryStream())
            //{
            //    image.Save(stream, new JpegEncoder());
            //    return File(stream.ToArray(), "image/jpeg");
            //}
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

        //private string GenerateCaptchaCode()
        //{
        //    // Genera un código de captcha aleatorio
        //    return Guid.NewGuid().ToString().Substring(0, 6);
        //}

        //private Image<Rgba32> GenerateCaptchaImage(string captchaCode)
        //{
        //    // Crea una imagen de captcha
        //    var image = new Image<Rgba32>(180, 50);

        //    // Dibuja el texto del captcha en la imagen
        //    image.Mutate(x => x.DrawImage(captchaCode, new FontFamily(SystemFonts.MessageBoxFont("Arial", 36)), Rgba32.Black, new PointF(10, 10)));

        //    // Agrega ruido a la imagen
        //    image.Mutate(x => x.Disperse(5));

        //    return image;
        //}
    }
}