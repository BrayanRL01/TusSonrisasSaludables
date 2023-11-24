using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class SpecialtiesHelper
    {
        private ServiceRepository repository;

        public SpecialtiesHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<SpecialtyViewModel>? GetAllView()
        {
            List<SpecialtyViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Specialties/Specialties/");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<SpecialtyViewModel>?>(content);
            }
            return list;
        }

        public SpecialtyViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Specialties/Specialty/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            SpecialtyViewModel? specialty = JsonConvert.DeserializeObject<SpecialtyViewModel?>(content);
            return specialty;
        }
        #endregion

        #region Update
        public string Edit(SpecialtyViewModel Specialty)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Specialties/Specialty/", Specialty);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Create
        public string Add(SpecialtyViewModel specialty)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Specialties/Specialty", specialty);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Specialties/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}
