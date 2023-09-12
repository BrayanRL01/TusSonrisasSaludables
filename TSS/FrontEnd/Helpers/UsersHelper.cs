using FrontEnd.Models;
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
            List<VWUserViewModel> lista = new List<VWUserViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/UsersView/");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                lista = JsonConvert.DeserializeObject<List<VWUserViewModel>>(content);
            }
            return lista;
        }

        //public List<VistaUsuarioViewModel> GetAllView()
        //{
        //    List<VistaUsuarioViewModel> lista = new List<VistaUsuarioViewModel>();
        //    HttpResponseMessage responseMessage = repository.GetResponse("api/VistaUsuario/");
        //    if (responseMessage != null)
        //    {
        //        var content = responseMessage.Content.ReadAsStringAsync().Result;
        //        lista = JsonConvert.DeserializeObject<List<VistaUsuarioViewModel>>(content);
        //    }
        //    return lista;
        //}
        #endregion

        #region GetByID
        public VWUserViewModel GetViewByID(int id)
        {
            VWUserViewModel Usuario = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/UsersView/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Usuario = JsonConvert.DeserializeObject<VWUserViewModel>(content);

            return Usuario;
        }
        #endregion

        #region Update
        //public UsuarioViewModel Edit(UsuarioViewModel Usuario)
        //{
        //    HttpResponseMessage responseMessage = repository.PutResponse("api/Usuario/", Usuario);
        //    var content = responseMessage.Content.ReadAsStringAsync().Result;
        //    UsuarioViewModel UsuarioAPI = JsonConvert.DeserializeObject<UsuarioViewModel>(content);
        //    return UsuarioAPI;
        //}
        #endregion

        #region Add
        public UserViewModel Add(UserViewModel Usuario)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Users/", Usuario);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UserViewModel UsuarioAPI = JsonConvert.DeserializeObject<UserViewModel>(content);
                return UsuarioAPI;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        //public UsuarioViewModel Delete(int id)
        //{
        //    UsuarioViewModel Usuario = new UsuarioViewModel();
        //    HttpResponseMessage responseMessage = repository.DeleteResponse("api/Usuario/" + id);
        //    return Usuario;
        //}
        #endregion
    }
}
