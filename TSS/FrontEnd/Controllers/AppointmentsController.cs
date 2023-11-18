using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontEnd.Controllers
{
    public class AppointmentsController : Controller
    {
        AppointmentsHelper appointmentsHelper = new();

        // GET: AppointmentsController
        public ActionResult Index()
        {
            List<VWAppointmentViewModel>? Appointments = appointmentsHelper.GetAppointmentsView();
            return View(Appointments);
        }

        // GET: AppointmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentsController/Edit/5
        [Authorize]
        public ActionResult Confirm(int id)
        {
            AppointmentViewModel? model = appointmentsHelper.GetByID(id);
            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(AppointmentViewModel model)
        {
            model.Email = User.FindFirst(ClaimTypes.Email)?.Value;
            string mensaje = appointmentsHelper.Confirm(model);

            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Index");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Cancel(int id)
        {
            AppointmentViewModel? model = appointmentsHelper.GetByID(id);
            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(AppointmentViewModel model)
        {
            model.Email = User.FindFirst(ClaimTypes.Email)?.Value;
            string mensaje = appointmentsHelper.Cancel(model);
            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = mensaje;
            return RedirectToAction(nameof(Index));
        }
    }
}
