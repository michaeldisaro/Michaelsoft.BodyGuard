using System;
using System.Net.Http;
using System.Text;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardBaseService
    {

        protected string BasePath;

        protected IHttpClientFactory HttpClientFactory;

        protected T PostRequest<T>(string url,
                                     dynamic requestObject)
        {
            using var client = HttpClientFactory.CreateClient();
            client.BaseAddress = new Uri($"{BasePath}");

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestObject),
                                                   Encoding.UTF8,
                                                   "application/json");

            var response = client.PostAsync(url, requestContent).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}