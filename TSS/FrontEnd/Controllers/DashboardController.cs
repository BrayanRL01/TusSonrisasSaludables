using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        #region Helpers

        UsersHelper Helper = new();
        CategoriesHelper categoriesHelper = new();
        GenresHelper genresHelper = new();
        IdentificationsHelper idHelper = new();
        ProvincesHelper provincesHelper = new();

        #endregion

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Users 
        public ActionResult Users()
        {
            List<VWUserViewModel> users = Helper.GetAllView();
            return View("Users/Index", users);
        }

        public ActionResult Details(int id)
        {
            VWUserViewModel vwuser = Helper.GetViewByID(id);
            return View("Users/Details", vwuser);
        }

        #region Create
        public ActionResult CreateAdmin()
        {
            UserViewModel user = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View("Users/CreateAdmin", user);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(UserViewModel user)
        {
            try
            {
                user = Helper.AddAdmin(user);
                return RedirectToAction("Users");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            UserViewModel user = Helper.GetByID(id);
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View("Users/Edit", user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            try
            {
                user = Helper.Edit(user);
                return RedirectToAction("Users");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            VWUserViewModel vwuser = Helper.GetViewByID(id);
            return View("Users/Delete", vwuser);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel user)
        {
            try
            {
                user = Helper.Delete(user.UserId);
                return RedirectToAction("Users");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #endregion

        #region Categories
        #region GetAll
        // GET: CategoriesController
        public ActionResult Categories()
        {
            List<VWCategoryViewModel> categories = categoriesHelper.GetCategoriesView();
            return View("Categories/Categories", categories);
        }

        public ActionResult SubCategories()
        {
            List<VWSubCategoryViewModel> subcategories = categoriesHelper.GetSubCategoriesView();
            return View("Categories/SubCategories", subcategories);
        }
        #endregion
        #endregion

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