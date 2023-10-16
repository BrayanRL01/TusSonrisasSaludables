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
        public List<VWRecordViewModel> GetAllView()
        {
            List<VWRecordViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/Records");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWRecordViewModel>>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public VWRecordViewModel GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/RecordInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWRecordViewModel record = JsonConvert.DeserializeObject<VWRecordViewModel>(content);

            return record;
        }

        public RecordViewModel GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Records/Record/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            RecordViewModel record = JsonConvert.DeserializeObject<RecordViewModel>(content);

            return record;
        }
        #endregion

        #region Update
        public RecordViewModel Edit(RecordViewModel record)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Records/Record/", record);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                RecordViewModel recordAPI = JsonConvert.DeserializeObject<RecordViewModel>(content);

                return recordAPI;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }
        #endregion

        #region Add
        public RecordViewModel Add(RecordViewModel Procedure)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Records/Record", Procedure);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                RecordViewModel recordAPI = JsonConvert.DeserializeObject<RecordViewModel>(content);
                return recordAPI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete
        public RecordViewModel Delete(int id)
        {
            RecordViewModel record = new();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Records/" + id);
            return record;
        }
        #endregion
    }
}
