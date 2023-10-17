using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class SecurityHelper
    {
        private ServiceRepository repository;

        public SecurityHelper()
        {
            repository = new ServiceRepository();
        }

        public TokenModel Login(LoginModel usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Authenticate/Login", new { usuario.Email, usuario.PasswordHash });
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                TokenModel TokenModel = JsonConvert.DeserializeObject<TokenModel>(content);

                return TokenModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserViewModel Register(UserViewModel Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Authenticate/Register", Usuario);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel UsuarioAPI = JsonConvert.DeserializeObject<UserViewModel>(content);
                return UsuarioAPI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginModel GetUser(LoginModel usuario)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Authenticate/GetUser", new { usuario.Email, usuario.PasswordHash });
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(content);
            return loginModel;
        }
    }
}
