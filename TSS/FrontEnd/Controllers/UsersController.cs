using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class UsersController : Controller
    {
        private UsersHelper usersHelper = new();
        private GenresHelper genresHelper = new();
        private IdentificationsHelper idHelper = new();
        private ProvincesHelper provincesHelper = new();


    }
}
