using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class SpecialtiesHelper
    {
        ServiceRepository repository;

        public SpecialtiesHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<SpecialtyViewModel> GetAllView()
        {
            List<SpecialtyViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Specialties/");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<SpecialtyViewModel>>(content);
            }
            return list;
        }

        public SpecialtyViewModel GetViewByID(int id)
        {
            SpecialtyViewModel Specialty = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Specialties/GetSpecialties/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Specialty = JsonConvert.DeserializeObject<SpecialtyViewModel>(content);

            return Specialty;
        }
        #endregion

        #region Update
        public SpecialtyViewModel Edit(SpecialtyViewModel Specialty)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Specialties/PutSpecialty/", Specialty);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            SpecialtyViewModel SpecialtyAPI = JsonConvert.DeserializeObject<SpecialtyViewModel>(content);

            return SpecialtyAPI;
        }
        #endregion

        #region Create
        public SpecialtyViewModel Add(SpecialtyViewModel specialty)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Specialties/PostSpecialties", specialty);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                SpecialtyViewModel SpecialtyAPI = JsonConvert.DeserializeObject<SpecialtyViewModel>(content);
                return SpecialtyAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public SpecialtyViewModel Delete(int id)
        {
            SpecialtyViewModel Specialty = new();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Specialties/" + id);
            return Specialty;
        }
        #endregion
    }
}
