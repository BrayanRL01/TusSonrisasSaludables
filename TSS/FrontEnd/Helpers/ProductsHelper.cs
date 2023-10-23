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
            List<VWProductViewModel> list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/Products");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWProductViewModel>>(content);
            }
            return list;
        }


        #region GetByID
        public VWProductViewModel GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/Product/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWProductViewModel Product = JsonConvert.DeserializeObject<VWProductViewModel>(content);

            return Product;
        }

        public ProductViewModel GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/GetProduct/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            ProductViewModel Product = JsonConvert.DeserializeObject<ProductViewModel>(content);

            return Product;
        }
        #endregion

        #region Update
        public ProductViewModel Edit(ProductViewModel Product)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Products/Product/", Product);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            ProductViewModel ProductAPI = JsonConvert.DeserializeObject<ProductViewModel>(content);

            return ProductAPI;
        }
        #endregion

        #region Create
        public ProductViewModel Add(ProductViewModel Product)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Products/Product", Product);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                ProductViewModel ProductAPI = JsonConvert.DeserializeObject<ProductViewModel>(content);
                return ProductAPI;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public ProductViewModel Delete(int id)
        {
            ProductViewModel Product = new ProductViewModel();
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Products/" + id);
            return Product;
        }
        #endregion
    }
}