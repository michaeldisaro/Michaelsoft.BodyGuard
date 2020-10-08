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
    public class BodyGuardUserApiService : BodyGuardBaseApiService
    {

        public BodyGuardUserApiService(IBodyGuardClientSettings settings,
                                       IHttpClientFactory httpClientFactory) :
            base(settings, httpClientFactory)
        {
        }

        public async Task<UsersDataResponse> GetUsers()
        {
            var baseApiResult = await GetRequest<UsersDataResponse>($"Users");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserDataResponse> GetUser(string id)
        {
            var baseApiResult = await GetRequest<UserDataResponse>($"User/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserUpdateResponse> UpdateUser(string id,
                                                         dynamic userData)
        {
            var userUpdateRequest = new UserUpdateRequest
            {
                Id = id,
                UserData = userData
            };

            var baseApiResult = await PutRequest<UserUpdateResponse>($"User/{id}", userUpdateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserDeleteResponse> DeleteUser(string id)
        {
            var baseApiResult = await DeleteRequest<UserDeleteResponse>($"User/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}