using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthenticationApiService : BodyGuardBaseApiService
    {

        public BodyGuardAuthenticationApiService(IBodyGuardClientSettings settings,
                                                 IHttpClientFactory httpClientFactory) :
            base(settings, httpClientFactory)
        {
        }

        public async Task<UserCreateResponse> Register(string email,
                                                       string password,
                                                       dynamic userData = null)
        {
            var userCreateRequest = new UserCreateRequest
            {
                EmailAddress = email,
                Password = password,
                UserData = userData
            };

            var baseApiResult = await PostRequest<UserCreateResponse>("Register", userCreateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}