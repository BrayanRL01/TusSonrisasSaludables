using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers
{
    public class GenresHelper
    {
        ServiceRepository repository;

        public GenresHelper()
        {
            repository = new ServiceRepository();
        }

        public List<GenreViewModel> GetAllView()
        {
            List<GenreViewModel> list = new List<GenreViewModel>();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Genres/GetGenresView");
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<GenreViewModel>>(content);
            }
            return list;
        }
    }
}
