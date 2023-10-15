using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class UsersHelper
    {
        ServiceRepository repository;

        public UsersHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWUserViewModel> GetAllView()
        {
            List<VWUserViewModel> list = new List<VWUserViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Users/GetUsersView");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWUserViewModel>>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public VWUserViewModel GetViewByID(int id)
        {
            VWUserViewModel Usuario = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Users/GetUserView/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Usuario = JsonConvert.DeserializeObject<VWUserViewModel>(content);

            return Usuario;
        }

        public UserViewModel GetByID(int id)
        {
            UserViewModel Usuario = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Users/GetUser/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Usuario = JsonConvert.DeserializeObject<UserViewModel>(content);

            return Usuario;
        }
        #endregion

        #region Update
        public UserViewModel Edit(UserViewModel User)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Users/PutUser/", User);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel UserAPI = JsonConvert.DeserializeObject<UserViewModel>(content);

                return UserAPI;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }
        #endregion

        #region Add
        public UserViewModel Add(UserViewModel Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Users/PostUser", Usuario);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel UsuarioAPI = JsonConvert.DeserializeObject<UserViewModel>(content);
                return UsuarioAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserViewModel AddAdmin(UserViewModel Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Users/PostAdminUser", Usuario);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel UsuarioAPI = JsonConvert.DeserializeObject<UserViewModel>(content);
                return UsuarioAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public UserViewModel Delete(int id)
        {
            UserViewModel User = new UserViewModel();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Users/" + id);
            return User;
        }
        #endregion
    }
}