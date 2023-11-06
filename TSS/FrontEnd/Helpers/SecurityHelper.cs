using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class SecurityHelper
    {
        private ServiceRepository _repository;

        public SecurityHelper()
        {
            _repository = new ServiceRepository();
        }

        public TokenModel? Login(LoginModel usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = _repository.PostResponse("api/Authenticate/Login", usuario);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                TokenModel? TokenModel = JsonConvert.DeserializeObject<TokenModel?>(content);

                return TokenModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserViewModel? Register(UserViewModel? Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = _repository.PostResponse("api/Authenticate/Register", Usuario!);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel? UsuarioAPI = JsonConvert.DeserializeObject<UserViewModel?>(content);
                return UsuarioAPI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserViewModel GetUser(LoginModel usuario)
        {
            HttpResponseMessage responseMessage = _repository.PostResponse("api/Authenticate/GetUser", usuario);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            UserViewModel loginModel = JsonConvert.DeserializeObject<UserViewModel>(content);
            return loginModel;
        }

        public string GetRole(LoginModel usuario)
        {
            HttpResponseMessage responseMessage = _repository.PostResponse("api/Authenticate/GetRole", usuario);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string Roles = content;
            return Roles;
        }

        public UserViewModel GetEmail(string email)
        {
            HttpResponseMessage responseMessage = _repository.GetResponse("api/Authenticate/GetEmail/" + email);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            UserViewModel user = JsonConvert.DeserializeObject<UserViewModel>(content);
            return user;
        }
    }
}
