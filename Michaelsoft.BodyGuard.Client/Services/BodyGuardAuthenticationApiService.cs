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
                                                 IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            BasePath = $"{settings.BasePath}/Authentication/";
        }

        public async Task<UserCreateResponse> UserCreate(string email,
                                                         string password,
                                                         dynamic userData)
        {
            var userCreateRequest = new UserCreateRequest
            {
                EmailAddress = email,
                Password = password,
                UserData = userData
            };

            var baseApiResult = await PostRequest<UserCreateResponse>("UserCreate", userCreateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserDataResponse> GetUserData(string id)
        {
            var baseApiResult = await GetRequest<UserDataResponse>($"GetUserData/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserUpdateResponse> UpdateUserData(string id,
                                                             dynamic userData)
        {
            var userUpdateRequest = new UserUpdateRequest
            {
                Id = id,
                UserData = userData
            };

            var baseApiResult = await PutRequest<UserUpdateResponse>($"UpdateUserData/{id}", userUpdateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserDeleteResponse> UserDelete(string id)
        {
            var baseApiResult = await GetRequest<UserDeleteResponse>($"UserDelete/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}