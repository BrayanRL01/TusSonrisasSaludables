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

    }
}
