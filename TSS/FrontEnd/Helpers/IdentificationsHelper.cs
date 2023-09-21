using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class IdentificationsHelper
    {
        ServiceRepository repository;

        public IdentificationsHelper()
        {
            repository = new ServiceRepository();
        }

        public List<IdentificationViewModel> GetAllView()
        {
            List<IdentificationViewModel> list = new List<IdentificationViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Identifications/GetIDTypesView");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<IdentificationViewModel>>(content);
            }
            return list;
        }
    }
}
