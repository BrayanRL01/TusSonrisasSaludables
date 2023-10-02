using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class BrandsHelper
    {
        ServiceRepository repository;

        public BrandsHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<BrandViewModel> GetBrandsView()
        {
            try
            {
                List<BrandViewModel> list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Brands/GetBrandsView");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<BrandViewModel>>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetByID
        public BrandViewModel GetViewByID(int id)
        {
            BrandViewModel Brand = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Brands/GetBrandView/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            Brand = JsonConvert.DeserializeObject<BrandViewModel>(content);

            return Brand;
        }
        #endregion
    }
}
