using Lab3MVC.Models;
using Lab3MVC.Models.Data;
using Lab3MVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab3MVC.Controllers
{
    public class Home2Controller : Controller
    {
       
        private readonly ILogger<Home2Controller> _logger;
        private readonly IConfiguration _configuration;
        


        public Home2Controller(ILogger<Home2Controller> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "dashboard.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Index_tables()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "tables.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Index_billing()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "billing.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Index_profile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "profile.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult Index_rtl()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "rtl.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult Index_sign_in()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sign-in.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Index_sign_up()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sign-up.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Index_virtual_reality()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "virtual-reality.html");

            if (System.IO.File.Exists(filePath))
            {
                return new PhysicalFileResult(filePath, "text/html");
            }
            else
            {
                return NotFound();
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