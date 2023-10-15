using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class SecurityHelper
    {
        ServiceRepository repository;

        public SecurityHelper()
        {
            repository = new ServiceRepository();
        }

        public TokenModel Login(LoginModel usuario)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Authenticate/Login", new { usuario.Email, usuario.PasswordHash });
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            TokenModel TokenModel = JsonConvert.DeserializeObject<TokenModel>(content);

            return TokenModel;
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
