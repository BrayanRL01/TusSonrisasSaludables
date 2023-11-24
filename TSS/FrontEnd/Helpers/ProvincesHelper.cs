using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class ProvincesHelper
    {
        ServiceRepository repository;

        public ProvincesHelper()
        {
            repository = new ServiceRepository();
        }

        public List<ProvinceViewModel>? GetAllView()
        {
            List<ProvinceViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Provinces/GetProvincesView");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ProvinceViewModel>?>(content);
            }
            return list;
        }
    }
}