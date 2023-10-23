using Newtonsoft.Json;
using System.Configuration;

namespace FrontEnd.Filters
{
    public class ReCaptchaService
    {
        private readonly IConfiguration _configuration;

        public ReCaptchaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> IsReCaptchaPassedAsync(string gRecaptchaResponse)
        {
            if (string.IsNullOrEmpty(gRecaptchaResponse))
            {
                return false;
            }

            using (var httpClient = new HttpClient())
            {
                var secretKey = _configuration["RecaptchaSettings:SecretKey"];
                var googleVerificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={gRecaptchaResponse}";

                var httpResponseMessage = await httpClient.GetAsync(googleVerificationUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(jsonResponse);

                    return reCaptchaResponse.Success;
                }
                else
                {
                    return false;
                }
            }
        }

        public class ReCaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }
    }
}