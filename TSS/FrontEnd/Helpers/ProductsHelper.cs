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

        public List<VWProductViewModel>? GetAllView()
        {
            List<VWProductViewModel>? list = new();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/Products");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<VWProductViewModel>?>(content);
            }
            return list;
        }


        #region GetByID
        public VWProductViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/Product/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWProductViewModel? Product = JsonConvert.DeserializeObject<VWProductViewModel?>(content);

            return Product;
        }

        public ProductViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Products/GetProduct/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            ProductViewModel? Product = JsonConvert.DeserializeObject<ProductViewModel?>(content);
            return Product;
        }
        #endregion

        #region Update
        public string Edit(ProductViewModel Product)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Products/Product/", Product);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region Create
        public string Add(ProductViewModel Product)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Products/Product", Product);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Delete
        public string Delete(int id)
        {
            HttpResponseMessage responseMessage = repository.DeleteResponse("api/Products/" + id);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion

        #region ChangeImage
        public string ChangeImage(ProductViewModel product)
        {
            HttpResponseMessage responseMessage = repository.PutResponse("api/Products/ChangeImage/", product);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            string mensaje = content;
            return mensaje;
        }
        #endregion
    }
}