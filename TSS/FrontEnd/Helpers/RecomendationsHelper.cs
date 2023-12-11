using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class RecomendationsHelper
    {
        ServiceRepository repository;

        public RecomendationsHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWRecomendationViewModel>? GetRecomendationsView()
        {
            try
            {
                List<VWRecomendationViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Recomendations/Recomendations");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWRecomendationViewModel>?>(content);
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetByID
        public RecomendationViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Recomendations/Recomendation/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            RecomendationViewModel? recomendation = JsonConvert.DeserializeObject<RecomendationViewModel?>(content);
            return recomendation;
        }
        #endregion

        #region Create
        public string Add(RecomendationViewModel Recomendation)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Recomendations/Recomendation/", Recomendation);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content.ToString();
            return mensaje;
        }
        #endregion

        #region Update
        public string Edit(RecomendationViewModel Recomendation)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Recomendations/Recomendation/", Recomendation);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Recomendations/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region ChangeImage
        public string ChangeImage(RecomendationViewModel recomendation)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Recomendations/ChangeImage/", recomendation);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}
