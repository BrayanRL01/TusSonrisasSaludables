namespace FrontEnd.Helpers
{
    public class ServiceRepository
    {
        public HttpClient Client { get; set; }

        public ServiceRepository()
        {
            Client = new()
            {
                BaseAddress = new Uri("https://localhost:7091")
            };
            Client.DefaultRequestHeaders.Add("ApiKey", "56CGE54GS94FS65V46F5BS6B1");
        }

        public ServiceRepository(string token)
        {
            Client = new()
            {
                BaseAddress = new Uri("https://localhost:7091/")
            };
            Client.DefaultRequestHeaders.Add("ApiKey", "56CGE54GS94FS65V46F5BS6B1");
            Client.DefaultRequestHeaders.Authorization =
              new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }

        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}