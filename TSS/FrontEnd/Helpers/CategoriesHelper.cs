using FrontEnd.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

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
        public List<VWCategoryViewModel>? GetCategoriesView()
        {
            try
            {
                List<VWCategoryViewModel>? list = new List<VWCategoryViewModel>();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/Categories");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWCategoryViewModel>?>(content);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<VWSubCategoryViewModel>? GetSubCategoriesView()
        {
            try
            {
                List<VWSubCategoryViewModel>? list = new();
                HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/SubCategories");
                if (responseMessage != null)
                {
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<VWSubCategoryViewModel>?>(content);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region GetID
        public VWCategoryViewModel? GetViewByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/Category/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWCategoryViewModel? category = JsonConvert.DeserializeObject<VWCategoryViewModel?>(content);

            return category;
        }

        public VWSubCategoryViewModel? GetSubByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/SubCategory/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            VWSubCategoryViewModel? subcategory = JsonConvert.DeserializeObject<VWSubCategoryViewModel?>(content);

            return subcategory;
        }

        public CategoryViewModel? GetByID(int id)
        {
            HttpResponseMessage responseMessage = repository.GetResponse("api/Categories/GetCategory/" + id);
            string content = responseMessage.Content.ReadAsStringAsync().Result;
            CategoryViewModel? category = JsonConvert.DeserializeObject<CategoryViewModel?>(content);

            return category;
        }
        #endregion

        #region Update
        public string EditCategory(CategoryViewModel category)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Categories/Category/", category);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditSubCategory(CategoryViewModel category)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PutResponse("api/Categories/SubCategory/", category);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Create
        public string AddCategory(CategoryViewModel category)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Categories/Category", category);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddSubCategory(CategoryViewModel category)
        {
            try
            {
                HttpResponseMessage responseMessage = repository.PostResponse("api/Categories/SubCategory", category);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return content;
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
            try
            {
                HttpResponseMessage responseMessage = repository.DeleteResponse("api/Categories/" + id);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                string mensaje = content;
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
