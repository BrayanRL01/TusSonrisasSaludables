using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class UsersController : Controller
    {
        UsersHelper Helper = new();
        GenresHelper genresHelper = new();
        IdentificationsHelper idHelper = new();
        ProvincesHelper provincesHelper = new();

        public ActionResult Index()
        {
            List<VWUserViewModel> users = Helper.GetAllView();
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            VWUserViewModel vwuser = new();
            vwuser = Helper.GetViewByID(id);
            return View(vwuser);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            UserViewModel user = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(user);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                user = Helper.Add(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateAdmin()
        {
            UserViewModel user = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(user);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(UserViewModel user)
        {
            try
            {
                user = Helper.AddAdmin(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            UserViewModel user = Helper.GetByID(id);
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            try
            {
                user = Helper.Edit(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            VWUserViewModel vwuser = new();
            vwuser = Helper.GetViewByID(id);
            return View(vwuser);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel user)
        {
            try
            {
                user = Helper.Delete(user.UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
