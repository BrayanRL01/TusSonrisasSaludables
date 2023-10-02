using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class ProductsHelper
    {
        ServiceRepository repository;

        public ProductsHelper()
        {
            repository = new ServiceRepository();
        }

        public List<VWProductViewModel> GetAllView()
        {
            List<VWProductViewModel> list = new List<VWProductViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/GetProductsView");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWProductViewModel>>(content);
            }
            return list;
        }
    }
}