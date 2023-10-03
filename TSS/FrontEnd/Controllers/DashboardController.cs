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

        #region Users
        UsersHelper Helper = new();
        GenresHelper genresHelper = new();
        IdentificationsHelper idHelper = new();
        ProvincesHelper provincesHelper = new();
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

        #region Appointments
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
                return RedirectToAction("Appointments");
            }
            catch
            {
                return View();
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
                return RedirectToAction("Appointments");
            }
            catch
            {
                return View();
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
                return RedirectToAction("Appointments");
            }
            catch
            {
                return View();
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
                return RedirectToAction("Specialties");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #endregion

        #region Brands
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
                return RedirectToAction("Brands");
            }
            catch
            {
                return View();
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
                return RedirectToAction("Brands");
            }
            catch
            {
                return View();
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
                return RedirectToAction("Brands");
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #endregion

        #region Doctors
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
                    doctor.DoctorPhoto = new byte[0];
                }

                doctor = doctorsHelper.Add(doctor);
                return RedirectToAction("Doctors");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View();
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
                    doctor.DoctorPhoto = new byte[0];
                }

                doctor = doctorsHelper.Edit(doctor);
                return RedirectToAction("Doctors");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View();
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
                doctor = doctorsHelper.Delete(doctor.DoctorId);
                return RedirectToAction("Doctors");
            }
            catch
            {
                return View();
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
        public ActionResult CreateProduct(ProductViewModel product)
        {
            try
            {
                product = productsHelper.Add(product);
                return RedirectToAction("Products");
            }
            catch
            {
                return View("Products");
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