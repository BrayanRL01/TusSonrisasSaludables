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

        #region Create
        public BrandViewModel Add(BrandViewModel Brand)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Brands/PostBrand/", Brand);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            BrandViewModel BrandAPI = JsonConvert.DeserializeObject<BrandViewModel>(content);
            return BrandAPI;
        }
        #endregion

        #region Update
        public BrandViewModel Edit(BrandViewModel Brand)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Brands/PutBrand/", Brand);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            BrandViewModel BrandAPI = JsonConvert.DeserializeObject<BrandViewModel>(content);

            return BrandAPI;
        }
        #endregion

        #region Delete
        public BrandViewModel Delete(int id)
        {
            BrandViewModel Brand = new();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Brands/" + id);
            return Brand;
        }
        #endregion
    }
}
