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
        public List<ProcedureViewModel> GetAllView()
        {
            List<ProcedureViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Procedures/Procedures");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ProcedureViewModel>>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public ProcedureViewModel GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Procedures/Procedure/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            ProcedureViewModel procedure = JsonConvert.DeserializeObject<ProcedureViewModel>(content);

            return procedure;
        }
        #endregion

        #region Update
        public ProcedureViewModel Edit(ProcedureViewModel Procedure)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Procedures/Procedure/", Procedure);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                ProcedureViewModel ProcedureAPI = JsonConvert.DeserializeObject<ProcedureViewModel>(content);

                return ProcedureAPI;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }
        #endregion

        #region Add
        public ProcedureViewModel Add(ProcedureViewModel Procedure)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Procedures/Procedure", Procedure);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                ProcedureViewModel procedureAPI = JsonConvert.DeserializeObject<ProcedureViewModel>(content);
                return procedureAPI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete
        public ProcedureViewModel Delete(int id)
        {
            ProcedureViewModel Procedure = new();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Procedures/" + id);
            return Procedure;
        }
        #endregion
    }
}
