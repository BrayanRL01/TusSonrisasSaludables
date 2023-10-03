using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        ProductsHelper productsHelper = new();
        BrandsHelper brandsHelper = new();
        CategoriesHelper categoriesHelper = new();

        #region Products
        public ActionResult Products()
        {
            List<VWProductViewModel> products = productsHelper.GetAllView();
            return View("Products/Products", products);
        }

        public ActionResult ProductDetails(int id)
        {
            VWProductViewModel product = productsHelper.GetViewByID(id);
            return View("Products/ProductDetails", product);
        }

        #region Create 
        public ActionResult CreateProduct()
        {
            ProductViewModel product = new();
            var brands = brandsHelper.GetBrandsView();
            var categories = categoriesHelper.GetSubCategoriesView();
            ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(categories, "CategoryId", "SubCategory");
            return View("Products/CreateProduct", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductViewModel product)
        {
            try
            {
                product = productsHelper.Add(product);
                return RedirectToAction("Products/Products");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit 
        public ActionResult EditProduct(int id)
        {
            ProductViewModel product = productsHelper.GetByID(id);
            var brands = brandsHelper.GetBrandsView();
            var categories = categoriesHelper.GetSubCategoriesView();
            ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName");
            ViewBag.Categories = new SelectList(categories, "CategoryId", "SubCategory");
            return View("Products/EditProduct", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductViewModel product)
        {
            try
            {
                product = productsHelper.Edit(product);
                return RedirectToAction("Products");
            }
            catch
            {
                return View("Products");
            }
        }
        #endregion

        #endregion
    }
}
