using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class UsersHelper
    {
        private ServiceRepository repository;

        public UsersHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWUserViewModel>? GetAllView()
        {
            try
            {
                List<VWUserViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Users/Users");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWUserViewModel>?>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetByID
        public VWUserViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Users/UserInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWUserViewModel? Usuario = JsonConvert.DeserializeObject<VWUserViewModel?>(content);
            return Usuario;
        }

        public UserViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Users/User/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            UserViewModel? Usuario = JsonConvert.DeserializeObject<UserViewModel?>(content);
            return Usuario;
        }
        #endregion

        #region Update
        public string Edit(UserViewModel User)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Users/User/", User);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Add
        public string AddAdmin(UserViewModel? Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Users/AdminUser", Usuario!);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string Mensaje = content;
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Delete
        public string? Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Users/" + id);
            var content = responseMessage.Content?.ReadAsStringAsync().Result;
            string? mensaje = content;
            return mensaje;
        }
        #endregion
    }
}