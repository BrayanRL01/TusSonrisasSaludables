using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        #region Helpers

        #region Users
        private UsersHelper Helper = new();
        private GenresHelper genresHelper = new();
        private IdentificationsHelper idHelper = new();
        private ProvincesHelper provincesHelper = new();
        #endregion

        #region Appointments
        AppointmentsHelper appointmentsHelper = new();
        SpecialtiesHelper specialtiesHelper = new();
        DoctorsHelper doctorsHelper = new();
        #endregion

        #region Categories
        CategoriesHelper categoriesHelper = new();
        #endregion

        #region Products
        ProductsHelper productsHelper = new();
        BrandsHelper brandsHelper = new();
        #endregion

        #region Records
        RecordsHelper recordsHelper = new();
        ProceduresHelper proceduresHelper = new();
        #endregion

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

        #region Get
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
        #endregion

        #region Create
        public ActionResult CreateAdmin()
        {
            UserViewModel user = new();
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
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
                TempData["Message"] = "Usuario creado correctamente.";
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se ha creado el usuario. " + ex.Message;
                return RedirectToAction("Users");
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
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
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
                TempData["Message"] = "Usuario modificado correctamente.";
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                TempData["Error"] = new Exception("Hubo un error: " + ex);
                return RedirectToAction("Users");
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
            user = Helper.Delete(user.UserId);
            if (user == null)
            {
                TempData["Message"] = "Usuario eliminado correctamente.";
                return RedirectToAction("Users");
            }

            TempData["Error"] = "No se eliminó el usuario:";
            return RedirectToAction("Users");
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

        #region Details
        public ActionResult CategoryDetails(int id)
        {
            VWCategoryViewModel category = categoriesHelper.GetViewByID(id);
            return View("Categories/CategoryDetails", category);
        }

        public ActionResult SubCategoryDetails(int id)
        {
            VWSubCategoryViewModel category = categoriesHelper.GetSubByID(id);
            return View("Categories/SubCategoryDetails", category);
        }
        #endregion

        #region Create
        public ActionResult CreateCategory()
        {
            CategoryViewModel category = new();
            return View("Categories/CreateCategory", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.AddCategory(category);
                TempData["Message"] = "Categoría creado correctamente.";
                return RedirectToAction("Categories");
            }
            catch (JsonReaderException ex)
            {
                TempData["Error"] = "No se pudo crear la categoría. " + ex.Message;
                return RedirectToAction("Categories");
            }
        }

        public ActionResult CreateSubCategory()
        {
            CategoryViewModel subcategory = new();
            var categories = categoriesHelper.GetCategoriesView();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View("Categories/CreateSubCategory", subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.AddSubCategory(category);
                TempData["Message"] = "Subcategoría creado correctamente.";
                return RedirectToAction("SubCategories");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear la subcategoría. " + ex.Message;
                return RedirectToAction("SubCategories");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditCategory(int id)
        {
            CategoryViewModel category = categoriesHelper.GetByID(id);
            return View("Categories/EditCategory", category);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.EditCategory(category);
                TempData["Message"] = "Categoría editada correctamente.";
                return RedirectToAction("Categories");
            }
            catch (SqlException ex)
            {
                TempData["Error"] = "No se pudo editar la categoría. " + ex.Message;
                return RedirectToAction("Categories");
            }
        }

        public ActionResult EditSubCategory(int id)
        {
            CategoryViewModel subcategory = categoriesHelper.GetByID(id);
            var categories = categoriesHelper.GetCategoriesView();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View("Categories/EditSubCategory", subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.EditSubCategory(category);
                TempData["Message"] = "Subcategoría creado correctamente.";
                return RedirectToAction("SubCategories");
            }
            catch (JsonReaderException ex)
            {
                TempData["Error"] = "No se pudo editar la subcategoría. " + ex.Message;
                return RedirectToAction("SubCategories");
            }
        }
        #endregion

        #region Delete
        public ActionResult DeleteCategory(int id)
        {
            VWCategoryViewModel category = categoriesHelper.GetViewByID(id);
            return View("Categories/DeleteCategory", category);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.Delete(category.CategoryId);
                TempData["Message"] = "Categoría eliminada correctamente.";
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Categories");
            }
        }

        public ActionResult DeleteSubCategory(int id)
        {
            VWSubCategoryViewModel category = categoriesHelper.GetSubByID(id);
            return View("Categories/DeleteSubCategory", category);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategory(CategoryViewModel category)
        {
            try
            {
                category = categoriesHelper.Delete(category.CategoryId);
                TempData["Message"] = "Subcategoría eliminada correctamente.";
                TempData["Status"] = "success";
                return RedirectToAction("SubCategories");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se eliminó la subcategoría: " + ex.Message;
                TempData["Status"] = "danger";
                return RedirectToAction("SubCategories");
            }
        }
        #endregion

        #endregion

        #region Appointments

        #region GetAll
        public ActionResult Appointments()
        {
            List<VWAdminAppointmentViewModel> adminAppointments = appointmentsHelper.GetAdminAppointmentsView();
            return View("Appointments/AdminAppointments", adminAppointments);
        }

        public ActionResult AppointmentDetails(int id)
        {
            VWAdminAppointmentViewModel Appointment = appointmentsHelper.GetViewByID(id);
            return View("Appointments/AppointmentDetails", Appointment);
        }
        #endregion

        #region Create
        public ActionResult CreateAppointment()
        {
            AppointmentViewModel appointment = new();
            var specialties = specialtiesHelper.GetAllView();
            var doctors = doctorsHelper.GetAllView();
            ViewBag.Specialties = new SelectList(specialties, "SpecialtyId", "SpecialtyName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            return View("Appointments/CreateAppointment", appointment);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment(AppointmentViewModel appointment)
        {
            try
            {
                appointment = appointmentsHelper.Add(appointment);
                TempData["Message"] = "Cita creada correctamente.";
                return RedirectToAction("Appointments");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Appointments");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditAppointment(int id)
        {
            AppointmentViewModel appointment = appointmentsHelper.GetByID(id);
            var specialties = specialtiesHelper.GetAllView();
            var doctors = doctorsHelper.GetAllView();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            ViewBag.Specialties = new SelectList(specialties, "SpecialtyId", "SpecialtyName");
            return View("Appointments/EditAppointment", appointment);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(AppointmentViewModel appointment)
        {
            try
            {
                appointment = appointmentsHelper.Edit(appointment);
                TempData["Message"] = "Cita editada correctamente.";
                return RedirectToAction("Appointments");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Appointments");
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteAppointment(int id)
        {
            VWAdminAppointmentViewModel vwappointment = appointmentsHelper.GetViewByID(id);
            return View("Appointments/DeleteAppointment", vwappointment);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(AppointmentViewModel appointment)
        {
            try
            {
                appointment = appointmentsHelper.Delete(appointment.AppointmentId);
                TempData["Message"] = "Cita eliminada correctamente.";
                return RedirectToAction("Appointments");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Appointments");
            }
        }
        #endregion

        #endregion

        #region Specialties

        #region Get
        public ActionResult Specialties()
        {
            List<SpecialtyViewModel> Specialties = specialtiesHelper.GetAllView();
            return View("Specialties/Specialties", Specialties);
        }

        public ActionResult SpecialtyDetails(int id)
        {
            SpecialtyViewModel Specialty = specialtiesHelper.GetViewByID(id);
            return View("Specialties/SpecialtyDetails", Specialty);
        }
        #endregion

        #region Create
        public ActionResult CreateSpecialty()
        {
            SpecialtyViewModel specialty = new();
            return View("Specialties/CreateSpecialty", specialty);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpecialty(SpecialtyViewModel specialty)
        {
            try
            {
                specialty = specialtiesHelper.Add(specialty);
                TempData["Message"] = "Especialidad creada.";
                return RedirectToAction("Specialties");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear la especialidad: " + ex.Message.ToString();
                return RedirectToAction("Specialties");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditSpecialty(int id)
        {
            SpecialtyViewModel specialty = specialtiesHelper.GetViewByID(id);
            return View("Specialties/EditSpecialty", specialty);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecialty(SpecialtyViewModel specialty)
        {
            try
            {
                specialty = specialtiesHelper.Edit(specialty);
                TempData["Message"] = "Especialidad editada correctamente.";
                return RedirectToAction("Specialties");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo editar la especialidad: " + ex.Message.ToString();
                return RedirectToAction("Specialties");
            }
        }
        #endregion

        #region Delete
        public ActionResult DeleteSpecialty(int id)
        {
            SpecialtyViewModel specialty = specialtiesHelper.GetViewByID(id);
            return View("Appointments/DeleteSpecialty", specialty);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSpecialty(SpecialtyViewModel specialty)
        {
            try
            {
                specialty = specialtiesHelper.Delete(specialty.SpecialtyId);
                TempData["Message"] = "Especialidad eliminada correctamente.";
                return RedirectToAction("Specialties");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Specialties");
            }
        }
        #endregion

        #endregion

        #region Brands

        #region GetAll
        public ActionResult Brands()
        {
            List<BrandViewModel> brands = brandsHelper.GetBrandsView();
            return View("Brands/Brands", brands);
        }

        public ActionResult BrandDetails(int id)
        {
            BrandViewModel brand = brandsHelper.GetViewByID(id);
            return View("Brands/BrandDetails", brand);
        }
        #endregion

        #region Create
        public ActionResult CreateBrand()
        {
            BrandViewModel brand = new();
            return View("Brands/CreateBrand", brand);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(BrandViewModel brand)
        {
            try
            {
                brand = brandsHelper.Add(brand);
                TempData["Message"] = "Marca creada correctamente.";
                return RedirectToAction("Brands");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo eliminar la marca: " + ex.Message;
                return RedirectToAction("Brands");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditBrand(int id)
        {
            BrandViewModel brand = brandsHelper.GetViewByID(id);
            return View("Brands/EditBrand", brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(BrandViewModel brand)
        {
            try
            {
                brand = brandsHelper.Edit(brand);
                TempData["Message"] = "Marca editada correctamente.";
                return RedirectToAction("Brands");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Brands");
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteBrand(int id)
        {
            BrandViewModel brand = brandsHelper.GetViewByID(id);
            return View("Brands/DeleteBrand", brand);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(BrandViewModel brand)
        {
            try
            {
                brand = brandsHelper.Delete(brand.BrandId);
                TempData["Message"] = "Marca eliminada correctamente.";
                return RedirectToAction("Brands");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo eliminar la marca: {ex.Message}";
                return RedirectToAction("Brands");
            }
        }
        #endregion

        #endregion

        #region Doctors

        #region GetAll
        public ActionResult Doctors()
        {
            List<VWDoctorViewModel> doctors = doctorsHelper.GetAllView();
            return View("Doctors/Doctors", doctors);
        }

        public ActionResult DoctorDetails(int id)
        {
            VWDoctorViewModel doctor = doctorsHelper.GetViewByID(id);
            return View("Doctors/DoctorDetails", doctor);
        }
        #endregion

        #region Create
        public ActionResult CreateDoctor()
        {
            DoctorViewModel doctor = new();
            var specialties = specialtiesHelper.GetAllView();
            var types = idHelper.GetAllView();
            var genres = genresHelper.GetAllView();
            ViewBag.Specialties = new SelectList(specialties, "SpecialtyId", "SpecialtyName");
            ViewBag.Types = new SelectList(types, "TypeId", "IdType");
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            return View("Doctors/CreateDoctor", doctor);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(DoctorViewModel doctor, List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    IFormFile formFile = files[0];

                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        doctor.DoctorPhoto = ms.ToArray();
                    }
                }
                else
                {
                    doctor.DoctorPhoto = Array.Empty<byte>();
                }

                doctor = doctorsHelper.Add(doctor);
                TempData["Message"] = "Doctor creado correctamente.";
                return RedirectToAction("Doctors");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo crear el doctor: {ex.Message}";
                return RedirectToAction("Doctors");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditDoctor(int id)
        {
            DoctorViewModel doctor = doctorsHelper.GetByID(id);
            var specialties = specialtiesHelper.GetAllView();
            var types = idHelper.GetAllView();
            var genres = genresHelper.GetAllView();
            ViewBag.Specialties = new SelectList(specialties, "SpecialtyId", "SpecialtyName");
            ViewBag.Types = new SelectList(types, "TypeId", "IdType");
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            return View("Doctors/EditDoctor", doctor);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctor(DoctorViewModel doctor, List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    IFormFile formFile = files[0];

                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        doctor.DoctorPhoto = ms.ToArray();
                    }
                }
                else
                {
                    doctor.DoctorPhoto = Array.Empty<byte>();
                }

                doctor = doctorsHelper.Edit(doctor);
                TempData["Message"] = $"Doctor {doctor.DoctorName} {doctor.FirstName} {doctor.LastName} editado correctamente.";
                return RedirectToAction("Doctors");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo editar el doctor: {ex.Message}";
                return RedirectToAction("Doctors");
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteDoctor(int id)
        {
            VWDoctorViewModel vwdoctor = doctorsHelper.GetViewByID(id);
            return View("Doctors/DeleteDoctor", vwdoctor);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDoctor(DoctorViewModel doctor)
        {
            try
            {
                TempData["Message"] = $"Doctor {doctor.DoctorName} {doctor.FirstName} {doctor.LastName} eliminado correctamente.";
                doctor = doctorsHelper.Delete(doctor.DoctorId);
                return RedirectToAction("Doctors");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Doctors");
            }
        }
        #endregion

        #endregion

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
        public ActionResult CreateProduct(ProductViewModel product, List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    IFormFile formFile = files[0];

                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        product.ProductImage = ms.ToArray();
                    }
                }
                else
                {
                    product.ProductImage = Array.Empty<byte>();
                }

                product = productsHelper.Add(product);
                TempData["Message"] = $"Producto {product.ProductName} creado correctamente.";
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Products");
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
        public ActionResult EditProduct(ProductViewModel product, List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    IFormFile formFile = files[0];

                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        product.ProductImage = ms.ToArray();
                    }
                }
                else
                {
                    product.ProductImage = Array.Empty<byte>();
                }
                product = productsHelper.Edit(product);
                TempData["Message"] = $"Producto {product.ProductName} editado correctamente.";
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Products");
            }
        }
        #endregion

        #region Delete
        public ActionResult DeleteProduct(int id)
        {
            VWProductViewModel product = productsHelper.GetViewByID(id);
            return View("Products/DeleteProduct", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(ProductViewModel product)
        {
            try
            {
                TempData["Message"] = $"Producto {product.ProductName} eliminado correctamente.";
                product = productsHelper.Delete(product.ProductId);
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo elimianr el producto {product.ProductName}: {ex.Message}";
                return RedirectToAction("Products");
            }
        }
        #endregion

        #endregion

        #region Procedures

        #region GetAll
        public ActionResult Procedures()
        {
            List<ProcedureViewModel> list = proceduresHelper.GetAllView();
            return View("Procedures/Procedures", list);
        }

        public ActionResult ProcedureDetails(int id)
        {
            ProcedureViewModel procedure = proceduresHelper.GetByID(id);
            return View("Procedures/ProcedureDetails", procedure);
        }
        #endregion

        #region Create
        public ActionResult CreateProcedure()
        {
            ProcedureViewModel procedure = new();
            return View("Procedures/CreateProcedure", procedure);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProcedure(ProcedureViewModel procedure)
        {
            try
            {
                procedure = proceduresHelper.Add(procedure);
                TempData["Message"] = $"Procedimiento {procedure.ProcedureName} creado correctamente.";
                return RedirectToAction("Procedures");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear el procedimiento: " + ex.Message;
                return RedirectToAction("Procedures");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditProcedure(int id)
        {
            ProcedureViewModel procedure = proceduresHelper.GetByID(id);
            return View("Procedures/EditProcedure", procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProcedure(ProcedureViewModel procedure)
        {
            try
            {
                procedure = proceduresHelper.Edit(procedure);
                TempData["Message"] = $"Procedimiento {procedure.ProcedureName} editado correctamente.";
                return RedirectToAction("Procedures");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Procedures");
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteProcedure(int id)
        {
            ProcedureViewModel procedure = proceduresHelper.GetByID(id);
            return View("Procedures/DeleteProcedure", procedure);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProcedure(ProcedureViewModel procedure)
        {
            try
            {
                procedure = proceduresHelper.Delete(procedure.ProcedureId);
                TempData["Message"] = "Procedimiento eliminado correctamente.";
                return RedirectToAction("Procedures");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo eliminar el procedimiento: {ex.Message}";
                return RedirectToAction("Procedures");
            }
        }
        #endregion

        #endregion

        #region Records

        #region GetAll
        public ActionResult Records()
        {
            List<VWRecordViewModel> list = recordsHelper.GetAllView();
            return View("Records/Records", list);
        }

        public ActionResult RecordDetails(int id)
        {
            VWRecordViewModel record = recordsHelper.GetViewByID(id);
            return View("Records/RecordDetails", record);
        }
        #endregion

        #region Create
        public ActionResult CreateRecord()
        {
            RecordViewModel record = new();
            var users = Helper.GetAllView();
            var doctors = doctorsHelper.GetAllView();
            var procedures = proceduresHelper.GetAllView();
            ViewBag.Users = new SelectList(users, "UserId", "FullName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            ViewBag.Procedures = new SelectList(procedures, "ProcedureId", "ProcedureName");
            return View("Records/CreateRecord", record);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecord(RecordViewModel record)
        {
            try
            {
                record = recordsHelper.Add(record);
                TempData["Message"] = $"Registro creado correctamente.";
                return RedirectToAction("Records");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear el registro: " + ex.Message;
                return RedirectToAction("Records");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditRecord(int id)
        {
            RecordViewModel record = recordsHelper.GetByID(id);
            List<VWUserViewModel> users = Helper.GetAllView();
            List<VWDoctorViewModel> doctors = doctorsHelper.GetAllView();
            List<ProcedureViewModel> procedures = proceduresHelper.GetAllView();
            ViewBag.Users = new SelectList(users, "UserId", "FullName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            ViewBag.Procedures = new SelectList(procedures, "ProcedureId", "ProcedureName");
            return View("Records/EditRecord", record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecord(RecordViewModel record)
        {
            try
            {
                record = recordsHelper.Edit(record);
                TempData["Message"] = "Registro editado correctamente.";
                return RedirectToAction("Records");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Records");
            }
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteRecord(int id)
        {
            VWRecordViewModel record = recordsHelper.GetViewByID(id);
            return View("Records/DeleteRecord", record);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecord(RecordViewModel record)
        {
            try
            {
                record = recordsHelper.Delete(record.RecordId);
                TempData["Message"] = "Registro eliminado correctamente.";
                return RedirectToAction("Procedures");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudo eliminar el registro: {ex.Message}";
                return RedirectToAction("Procedures");
            }
        }
        #endregion
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EditAdmin()
        {
            SecurityHelper securityHelper = new SecurityHelper();
            string email = securityHelper.GetEmail();
            UserViewModel user = securityHelper.GetByEmail(email);
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View("EditAdmin", user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAdmin(UserViewModel user)
        {
            try
            {
                user = Helper.Edit(user);
                TempData["Message"] = "Usuario modificado correctamente.";
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                TempData["Error"] = new Exception("Hubo un error: " + ex);
                return RedirectToAction("Users");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}