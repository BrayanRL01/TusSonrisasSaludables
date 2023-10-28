using Aspose.Pdf.Operators;
using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            List<VWAdminAppointmentViewModel> Appointments = appointmentsHelper.GetAdminAppointmentsView();
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
            AppointmentViewModel model = appointmentsHelper.GetByID(id);
            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(AppointmentViewModel model)
        {
            try
            {
                model.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                appointmentsHelper.Confirm(model);
                TempData["Message"] = "Cita confirmada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Usted no es la persona a nombre de esta cita. " + ex.Message;
                return View("Index");
            }
        }

        [Authorize]
        public ActionResult Cancel(int id)
        {
            AppointmentViewModel model = appointmentsHelper.GetByID(id);
            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(AppointmentViewModel model)
        {
            try
            {
                model.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                appointmentsHelper.Cancel(model);
                TempData["Message"] = "Cita cancelada correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
