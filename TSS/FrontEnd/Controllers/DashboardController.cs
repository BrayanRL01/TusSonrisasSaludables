using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        #region Helpers
        private ServiceRepository repository = new();
        private SecurityHelper securityHelper = new();

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

        private RecomendationsHelper recomendationsHelper = new();

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
            List<VWUserViewModel>? users = Helper.GetAllView();
            return View("Users/Users", users);
        }

        public ActionResult Details(int id)
        {
            VWUserViewModel? vwuser = Helper.GetViewByID(id);
            return View("Users/Details", vwuser);
        }
        #endregion

        #region Create
        public IActionResult CreateAdmin()
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
        public IActionResult CreateAdmin(UserViewModel user)
        {
            string mensaje = Helper.AddAdmin(user);
            if (mensaje.StartsWith("A"))
            {
                TempData["Message"] = mensaje;
                //TempData["Message"] = "Usuario creado correctamente.";
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = mensaje;
                return RedirectToAction("Users");
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            UserViewModel? user = Helper.GetByID(id);
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
            string mensaje = Helper.Edit(user);

            if (mensaje.StartsWith("U"))
            {
                TempData["Message"] = "Usuario modificado correctamente.";
                return RedirectToAction("Users");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Users");
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            VWUserViewModel? vwuser = Helper.GetViewByID(id);
            return View("Users/Delete", vwuser);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel user)
        {
            string mensaje = Helper.Delete(user.UserId);
            if (mensaje!.StartsWith("U"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Users");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Users");
        }

        #endregion

        #endregion

        #region Categories

        #region GetAll
        // GET: CategoriesController
        public ActionResult Categories()
        {
            List<VWCategoryViewModel>? categories = categoriesHelper.GetCategoriesView();
            return View("Categories/Categories", categories);
        }

        public ActionResult SubCategories()
        {
            List<VWSubCategoryViewModel>? subcategories = categoriesHelper.GetSubCategoriesView();
            return View("Categories/SubCategories", subcategories);
        }
        #endregion

        #region Details
        public ActionResult CategoryDetails(int id)
        {
            VWCategoryViewModel? category = categoriesHelper.GetViewByID(id);
            return View("Categories/CategoryDetails", category);
        }

        public ActionResult SubCategoryDetails(int id)
        {
            VWSubCategoryViewModel? category = categoriesHelper.GetSubByID(id);
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
            string mensaje = categoriesHelper.AddCategory(category);
            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Categories");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Categories");

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
            string mensaje = categoriesHelper.AddSubCategory(category);

            if (mensaje.StartsWith("S"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("SubCategories");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("SubCategories");

        }
        #endregion

        #region Edit
        public ActionResult EditCategory(int id)
        {
            CategoryViewModel? category = categoriesHelper.GetByID(id);
            return View("Categories/EditCategory", category);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            string mensaje = categoriesHelper.EditCategory(category);

            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Categories");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Categories");
        }

        public ActionResult EditSubCategory(int id)
        {
            CategoryViewModel? subcategory = categoriesHelper.GetByID(id);
            var categories = categoriesHelper.GetCategoriesView();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View("Categories/EditSubCategory", subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(CategoryViewModel category)
        {
            string mensaje = categoriesHelper.EditSubCategory(category);

            if (mensaje.StartsWith("S"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("SubCategories");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("SubCategories");

        }
        #endregion

        #region Delete
        public ActionResult DeleteCategory(int id)
        {
            VWCategoryViewModel? category = categoriesHelper.GetViewByID(id);
            return View("Categories/DeleteCategory", category);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(CategoryViewModel category)
        {
            string mensaje = categoriesHelper.Delete(category.CategoryId);

            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Categories");
            }
            TempData["Error"] = mensaje;
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteSubCategory(int id)
        {
            VWSubCategoryViewModel? category = categoriesHelper.GetSubByID(id);
            return View("Categories/DeleteSubCategory", category);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategory(CategoryViewModel category)
        {
            string mensaje = categoriesHelper.Delete(category.CategoryId);
            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("SubCategories");
            }
            TempData["Error"] = mensaje;
            return RedirectToAction("SubCategories");
        }
        #endregion

        #endregion

        #region Appointments

        #region GetAll
        public ActionResult Appointments()
        {
            List<VWAdminAppointmentViewModel>? adminAppointments = appointmentsHelper.GetAdminAppointmentsView();
            return View("Appointments/AdminAppointments", adminAppointments);
        }

        public ActionResult AppointmentDetails(int id)
        {
            VWAdminAppointmentViewModel? Appointment = appointmentsHelper.GetViewByID(id);
            return View("Appointments/AppointmentDetails", Appointment);
        }
        #endregion

        #region Create
        public ActionResult CreateAppointment()
        {
            var citas = new List<AppointmentViewModel>();

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
        public ActionResult CreateAppointment(AppointmentViewModel appointments, string mensaje)
        {

            var startTimes = Request.Form["StartTime"];
            if (appointments.DoctorId != null && appointments?.SpecialtyId != null)
            {
                foreach (var startTime in startTimes)
                {
                    var appointment = new AppointmentViewModel
                    {
                        DoctorId = appointments.DoctorId,
                        SpecialtyId = appointments.SpecialtyId,
                        StartTime = DateTime.Parse(startTime),
                        EndTime = DateTime.Parse(startTime).AddHours(1),
                    };

                    mensaje = appointmentsHelper.Add(appointment);
                };
            }
            else
            {
                TempData["Error"] = "Se debe escoger un doctor y una especialidad.";
                return RedirectToAction("Appointments");
            }

            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Appointments");

            }
            else
            {
                TempData["Error"] = mensaje;
                return RedirectToAction("Appointments");
            }
        }
        #endregion

        #region Edit
        public ActionResult EditAppointment(int id)
        {
            AppointmentViewModel? appointment = appointmentsHelper.GetByID(id);
            var specialties = specialtiesHelper.GetAllView();
            var doctors = doctorsHelper.GetAllView();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            ViewBag.Specialties = new SelectList(specialties, "SpecialtyId", "SpecialtyName");
            return View("Appointments/EditAppointment", appointment);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(AppointmentViewModel? appointment)
        {
            string mensaje = appointmentsHelper.Edit(appointment!);
            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = "Cita editada correctamente.";
                return RedirectToAction("Appointments");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Appointments");

        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteAppointment(int id)
        {
            VWAdminAppointmentViewModel? vwappointment = appointmentsHelper.GetViewByID(id);
            return View("Appointments/DeleteAppointment", vwappointment);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(AppointmentViewModel appointment)
        {
            string mensaje = appointmentsHelper.Delete(appointment.AppointmentId);
            if (mensaje.StartsWith("C"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Appointments");
            }
            TempData["Error"] = mensaje;
            return RedirectToAction("Appointments");
        }
        #endregion

        #endregion

        #region Specialties

        #region Get
        public ActionResult Specialties()
        {
            List<SpecialtyViewModel>? Specialties = specialtiesHelper.GetAllView();
            return View("Specialties/Specialties", Specialties);
        }

        public ActionResult SpecialtyDetails(int id)
        {
            SpecialtyViewModel? Specialty = specialtiesHelper.GetViewByID(id);
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
            string mensaje = specialtiesHelper.Add(specialty);

            if (mensaje.StartsWith("E"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Specialties");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Specialties");

        }
        #endregion

        #region Edit
        public ActionResult EditSpecialty(int id)
        {
            SpecialtyViewModel? specialty = specialtiesHelper.GetViewByID(id);
            return View("Specialties/EditSpecialty", specialty);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecialty(SpecialtyViewModel specialty)
        {
            string mensaje = specialtiesHelper.Edit(specialty);

            if (mensaje.StartsWith("E"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Specialties");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Specialties");
        }
        #endregion

        #region Delete
        public ActionResult DeleteSpecialty(int id)
        {
            SpecialtyViewModel? specialty = specialtiesHelper.GetViewByID(id);
            return View("Specialties/DeleteSpecialty", specialty);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSpecialty(SpecialtyViewModel specialty)
        {
            string mensaje = specialtiesHelper.Delete(specialty.SpecialtyId);

            if (mensaje.StartsWith("E"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Specialties");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Specialties");
        }
        #endregion

        #endregion

        #region Brands

        #region GetAll
        public ActionResult Brands()
        {
            List<BrandViewModel>? brands = brandsHelper.GetBrandsView();
            return View("Brands/Brands", brands);
        }

        public ActionResult BrandDetails(int id)
        {
            BrandViewModel? brand = brandsHelper.GetViewByID(id);
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
            string mensaje = brandsHelper.Add(brand);

            if (mensaje.StartsWith("M"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Brands");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Brands");
        }
        #endregion

        #region Edit
        public ActionResult EditBrand(int id)
        {
            BrandViewModel? brand = brandsHelper.GetViewByID(id);
            return View("Brands/EditBrand", brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand(BrandViewModel brand)
        {
            string mensaje = brandsHelper.Edit(brand);

            if (mensaje.StartsWith("M"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Brands");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Brands");
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteBrand(int id)
        {
            BrandViewModel? brand = brandsHelper.GetViewByID(id);
            return View("Brands/DeleteBrand", brand);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(BrandViewModel brand)
        {
            string mensaje = brandsHelper.Delete(brand.BrandId);

            if (mensaje.StartsWith("M"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Brands");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Brands");
        }
        #endregion

        #endregion

        #region Doctors

        #region GetAll
        public ActionResult Doctors()
        {
            List<VWDoctorViewModel>? doctors = doctorsHelper.GetAllView();
            return View("Doctors/Doctors", doctors);
        }

        public ActionResult DoctorDetails(int id)
        {
            VWDoctorViewModel? doctor = doctorsHelper.GetViewByID(id);
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

        public ActionResult CreateDoctors()
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
            if (files.Count > 0)
            {
                IFormFile formFile = files[0];

                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                doctor.DoctorPhoto = ms.ToArray();
            }
            else
            {
                doctor.DoctorPhoto = Array.Empty<byte>();
            }

            var mensaje = doctorsHelper.Add(doctor);

            if (mensaje.StartsWith("D"))
            {
                TempData["Message"] = mensaje.ToString();
                return RedirectToAction("Doctors");
            }

            TempData["Error"] = mensaje.ToString();
            return RedirectToAction("Doctors");
        }
        #endregion

        #region Edit
        public ActionResult EditDoctor(int id)
        {
            DoctorViewModel? doctor = doctorsHelper.GetByID(id);
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
            if (files.Count > 0)
            {
                IFormFile formFile = files[0];

                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                doctor.DoctorPhoto = ms.ToArray();
            }
            else
            {
                doctor.DoctorPhoto = Array.Empty<byte>();
            }

            string mensaje = doctorsHelper.Edit(doctor);

            if (mensaje.StartsWith("D"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Doctors");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Doctors");
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteDoctor(int id)
        {
            VWDoctorViewModel? vwdoctor = doctorsHelper.GetViewByID(id);
            return View("Doctors/DeleteDoctor", vwdoctor);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDoctor(DoctorViewModel doctor)
        {
            string mensaje = doctorsHelper.Delete(doctor.DoctorId);

            if (mensaje.StartsWith("D"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Doctors");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Doctors");
        }
        #endregion

        #endregion

        #region Products
        public ActionResult Products()
        {
            List<VWProductViewModel>? products = productsHelper.GetAllView();
            return View("Products/Products", products);
        }

        public ActionResult ProductDetails(int id)
        {
            VWProductViewModel? product = productsHelper.GetViewByID(id);
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
            if (files.Count > 0)
            {
                IFormFile formFile = files[0];

                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                product.ProductImage = ms.ToArray();
            }
            else
            {
                product.ProductImage = Array.Empty<byte>();
            }

            string mensaje = productsHelper.Add(product);
            if (mensaje.StartsWith("P"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Products");
            }
            TempData["Error"] = mensaje;
            return RedirectToAction("Products");
        }
        #endregion

        #region Edit 
        public ActionResult EditProduct(int id)
        {
            ProductViewModel? product = productsHelper.GetByID(id);
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
            if (files.Count > 0)
            {
                IFormFile formFile = files[0];

                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                product.ProductImage = ms.ToArray();
            }
            else
            {
                product.ProductImage = Array.Empty<byte>();
            }

            string mensaje = productsHelper.Edit(product);
            if (mensaje.StartsWith("P"))
            {
                TempData["Message"] = $"Producto {product.ProductName} editado correctamente.";
                return RedirectToAction("Products");
            }
            TempData["Error"] = mensaje;
            return RedirectToAction("Products");
        }
        #endregion

        #region Delete
        public ActionResult DeleteProduct(int id)
        {
            VWProductViewModel? product = productsHelper.GetViewByID(id);
            return View("Products/DeleteProduct", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(ProductViewModel product)
        {
            string mensaje = productsHelper.Delete(product.ProductId);

            if (mensaje.StartsWith("P"))
            {

                TempData["Message"] = $"Producto {product.ProductName} eliminado correctamente.";
                return RedirectToAction("Products");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Products");
        }
        #endregion

        #endregion

        #region Procedures

        #region GetAll
        public ActionResult Procedures()
        {
            List<ProcedureViewModel>? list = proceduresHelper.GetAllView();
            return View("Procedures/Procedures", list);
        }

        public ActionResult ProcedureDetails(int id)
        {
            ProcedureViewModel? procedure = proceduresHelper.GetByID(id);
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
            string mensaje = proceduresHelper.Add(procedure);

            if (mensaje.StartsWith("P"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Procedures");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Procedures");

        }
        #endregion

        #region Edit
        public ActionResult EditProcedure(int id)
        {
            ProcedureViewModel? procedure = proceduresHelper.GetByID(id);
            return View("Procedures/EditProcedure", procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProcedure(ProcedureViewModel procedure)
        {
            string mensaje = proceduresHelper.Edit(procedure);

            if (mensaje.StartsWith("P"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Procedures");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Procedures");
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteProcedure(int id)
        {
            ProcedureViewModel? procedure = proceduresHelper.GetByID(id);
            return View("Procedures/DeleteProcedure", procedure);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProcedure(ProcedureViewModel procedure)
        {
            string mensaje = proceduresHelper.Delete(procedure.ProcedureId);

            if (mensaje.StartsWith("P"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Procedures");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Procedures");
        }
        #endregion

        #endregion

        #region Records

        #region GetAll
        public ActionResult Records()
        {
            List<VWRecordViewModel>? list = recordsHelper.GetAllView();
            return View("Records/Records", list);
        }

        public ActionResult RecordDetails(int id)
        {
            VWRecordViewModel? record = recordsHelper.GetViewByID(id);
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
            string mensaje = recordsHelper.Add(record);
            if (mensaje.StartsWith("R"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Records");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Records");
        }
        #endregion

        #region Edit
        public ActionResult EditRecord(int id)
        {
            RecordViewModel? record = recordsHelper.GetByID(id);
            List<VWUserViewModel>? users = Helper.GetAllView();
            List<VWDoctorViewModel>? doctors = doctorsHelper.GetAllView();
            List<ProcedureViewModel>? procedures = proceduresHelper.GetAllView();
            ViewBag.Users = new SelectList(users, "UserId", "FullName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FullName");
            ViewBag.Procedures = new SelectList(procedures, "ProcedureId", "ProcedureName");
            return View("Records/EditRecord", record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecord(RecordViewModel record)
        {
            string mensaje = recordsHelper.Edit(record);

            if (mensaje.StartsWith("R"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Records");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Records");
        }
        #endregion

        #region Delete
        // GET: UsersController/Delete/5
        public ActionResult DeleteRecord(int id)
        {
            VWRecordViewModel? record = recordsHelper.GetViewByID(id);
            return View("Records/DeleteRecord", record);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecord(RecordViewModel record)
        {
            string mensaje = recordsHelper.Delete(record.RecordId);

            if (mensaje.StartsWith("R"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Records");
            }

            TempData["Error"] = mensaje;
            return RedirectToAction("Records");
        }
        #endregion

        #endregion

        #region
        public ActionResult Blog()
        {
            return View("Blog/Blog");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            UserViewModel? user = securityHelper.GetEmail(email!);
            var genres = genresHelper.GetAllView();
            var ids = idHelper.GetAllView();
            var provinces = provincesHelper.GetAllView();
            ViewBag.Genres = new SelectList(genres, "GenreId", "GenreName");
            ViewBag.IDTypes = new SelectList(ids, "TypeId", "IdType");
            ViewBag.Provinces = new SelectList(provinces, "ProvinceId", "ProvinceName");
            return View("EditProfile", user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserViewModel user)
        {
            string mensaje = Helper.Edit(user);

            if (mensaje.StartsWith("U"))
            {
                TempData["Message"] = mensaje;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = mensaje;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ChangePassword()
        {
            PasswordModel model = new();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(PasswordModel model)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            model.Email = email!;
            string message = securityHelper.ChangePassword(model);
            if (message.StartsWith("Se"))
            {
                TempData["Message"] = message;
                return RedirectToAction("ChangePassword");
            }
            else
            {
                TempData["Error"] = message;
                return RedirectToAction("ChangePassword");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Blogs()
        {
            List<VWRecomendationViewModel>? recomendations = recomendationsHelper.GetRecomendationsView();
            return View("Blog/Blog", recomendations);
        }
    }
}