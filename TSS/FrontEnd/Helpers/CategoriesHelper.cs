using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class CategoriesHelper
    {
        ServiceRepository repository;

        public CategoriesHelper()
        {
            repository = new ServiceRepository();
        }

        #region GetAll
        public List<VWCategoryViewModel> GetCategoriesView()
        {
            try
            {
                List<VWCategoryViewModel> list = new List<VWCategoryViewModel>();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/GetCategoriesView");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWCategoryViewModel>>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<VWSubCategoryViewModel> GetSubCategoriesView()
        {
            try
            {
                List<VWSubCategoryViewModel> list = new List<VWSubCategoryViewModel>();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/GetSubCategoriesView");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWSubCategoryViewModel>>(content);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
