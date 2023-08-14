using Lab3MVC.Models;
using Lab3MVC.Models.Data;
using Lab3MVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab3MVC.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        StudentDAO studentDAO;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Insert([FromBody] Student student)
        {
            studentDAO = new StudentDAO(_configuration);
            return View();
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