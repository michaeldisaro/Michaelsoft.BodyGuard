using System;
using System.Net.Http;
using System.Text;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthenticationApiService : BodyGuardBaseService
    {

        public BodyGuardAuthenticationApiService(IBodyGuardClientSettings settings,
                                                 IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            BasePath = $"{settings.BasePath}/Authentication/";
        }

        public UserCreateResponse UserCreate(string email,
                                             string password,
                                             dynamic userData)
        {
            var userCreateRequest = new UserCreateRequest
            {
                EmailAddress = email,
                Password = password,
                UserData = userData
            };

            return PostRequest<UserCreateResponse>("UserCreate", userCreateRequest);
        }

    }
}