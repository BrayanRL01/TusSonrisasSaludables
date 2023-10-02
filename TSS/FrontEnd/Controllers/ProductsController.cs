using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        ProductsHelper Helper = new();
        GenresHelper genresHelper = new();
        IdentificationsHelper idHelper = new();
        ProvincesHelper provincesHelper = new();

        public ActionResult Index()
        {
            List<VWProductViewModel> products = Helper.GetAllView();
            return View(products);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            VWProductViewModel vwproduct = new();
            vwproduct = Helper.GetViewByID(id);
            return View(vwproduct);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            ProductViewModel product = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(product);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                product = Helper.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateAdmin()
        {
            ProductViewModel product = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "Idtype");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View(product);
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductViewModel user = Helper.GetByID(id);
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
        public ActionResult Edit(ProductViewModel product)
        {
            try
            {
                product = Helper.Edit(product);
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
            VWProductViewModel vwproduct = new();
            vwproduct = Helper.GetViewByID(id);
            return View(vwproduct);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductViewModel product)
        {
            try
            {
                product = Helper.Delete(product.ProductID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
