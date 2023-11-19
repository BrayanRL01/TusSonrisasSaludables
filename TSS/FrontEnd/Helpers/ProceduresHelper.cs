using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class ProceduresHelper
    {
        private ServiceRepository repository;

        public ProceduresHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<ProcedureViewModel>? GetAllView()
        {
            List<ProcedureViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Procedures/Procedures");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ProcedureViewModel>?>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public ProcedureViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Procedures/Procedure/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            ProcedureViewModel? procedure = JsonConvert.DeserializeObject<ProcedureViewModel?>(content);

            return procedure;
        }
        #endregion

        #region Update
        public string Edit(ProcedureViewModel Procedure)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Procedures/Procedure/", Procedure);
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
        public string Add(ProcedureViewModel Procedure)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Procedures/Procedure", Procedure);
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

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Procedures/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}
