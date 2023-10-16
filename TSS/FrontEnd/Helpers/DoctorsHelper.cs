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
        public List<VWDoctorViewModel> GetAllView()
        {
            List<VWDoctorViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/Doctors/");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWDoctorViewModel>>(content);
            }
            return list;
        }
        #endregion

        #region GetByID
        public DoctorViewModel GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/Doctor/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            DoctorViewModel Doctor = JsonConvert.DeserializeObject<DoctorViewModel>(content);
            return Doctor;
        }

        public VWDoctorViewModel GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Doctor/DoctorInfo/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWDoctorViewModel Doctor = JsonConvert.DeserializeObject<VWDoctorViewModel>(content);

            return Doctor;
        }
        #endregion

        #region Update
        public DoctorViewModel Edit(DoctorViewModel Doctor)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Doctor/Doctor/", Doctor);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            DoctorViewModel DoctorAPI = JsonConvert.DeserializeObject<DoctorViewModel>(content);
            return DoctorAPI;
        }
        #endregion

        #region Add
        public DoctorViewModel Add(DoctorViewModel Doctor)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Doctor/Doctor", Doctor);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                DoctorViewModel DoctorAPI = JsonConvert.DeserializeObject<DoctorViewModel>(content);
                return DoctorAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public DoctorViewModel Delete(int id)
        {
            DoctorViewModel Doctor = new();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Doctor/" + id);
            return Doctor;
        }
        #endregion
    }
}
