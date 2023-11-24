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
        public List<BrandViewModel>? GetBrandsView()
        {
            try
            {
                List<BrandViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Brands/Brands");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<BrandViewModel>?>(content);
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
        public BrandViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Brands/Brand/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            BrandViewModel? Brand = JsonConvert.DeserializeObject<BrandViewModel?>(content);
            return Brand;
        }
        #endregion

        #region Create
        public string Add(BrandViewModel Brand)
        {
            HttpResponseMessage responseMessage = repository.PostResponse("api/Brands/Brand/", Brand);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content.ToString();
            return mensaje;
        }
        #endregion

        #region Update
        public string Edit(BrandViewModel Brand)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Brands/Brand/", Brand);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Brands/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}
