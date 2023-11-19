using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class DoctorsHelper
    {
        ServiceRepository repository;

        public DoctorsHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWDoctorViewModel>? GetAllView()
        {
            List<VWDoctorViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/Doctors/");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWDoctorViewModel>?>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public DoctorViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/Doctor/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            DoctorViewModel? Doctor = JsonConvert.DeserializeObject<DoctorViewModel?>(content);
            return Doctor;
        }

        public VWDoctorViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/DoctorInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWDoctorViewModel? Doctor = JsonConvert.DeserializeObject<VWDoctorViewModel?>(content);

            return Doctor;
        }
        #endregion

        #region Update
        public string Edit(DoctorViewModel Doctor)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Doctor/Doctor/", Doctor);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Add
        public string Add(DoctorViewModel Doctor)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Doctor/Doctor", Doctor);
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
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Doctor/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}
