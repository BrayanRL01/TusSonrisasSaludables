using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class RecordsHelper
    {
        private ServiceRepository repository;

        public RecordsHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWRecordViewModel>? GetAllView()
        {
            List<VWRecordViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/Records");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWRecordViewModel>?>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public VWRecordViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/RecordInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWRecordViewModel? record = JsonConvert.DeserializeObject<VWRecordViewModel?>(content);
            return record;
        }

        public RecordViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/Record/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            RecordViewModel? record = JsonConvert.DeserializeObject<RecordViewModel?>(content);
            return record;
        }

        public List<VWRecordViewModel>? GetUserRecords(string email)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.GetResponse("api/Records/UserRecords/" + email);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                List<VWRecordViewModel>? Appointment = JsonConvert.DeserializeObject<List<VWRecordViewModel>?>(content);
                return Appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update
        public string Edit(RecordViewModel record)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Records/Record/", record);
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
        public string Add(RecordViewModel record)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Records/Record", record);
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
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Records/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;

        }
        #endregion
    }
}
