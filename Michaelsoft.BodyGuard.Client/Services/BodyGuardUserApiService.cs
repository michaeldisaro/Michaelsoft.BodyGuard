using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardUserApiService : BodyGuardBaseApiService, IBodyGuardUserApiService
    {

        public BodyGuardUserApiService(IBodyGuardClientSettings settings,
                                       IHttpClientFactory httpClientFactory,
                                       IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<UserList> GetUsers()
        {
            var baseApiResult = await GetRequest<UsersDataResponse>($"Users");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            var usersDataResponse = (UsersDataResponse) baseApiResult.Response;
            if (!usersDataResponse.Success)
                throw new Exception(usersDataResponse.Message);

            // parse response
            var usersData = new List<UserData>();
            foreach (var (id, data) in usersDataResponse.UsersData)
            {
                var userData = data.IsNullOrEmpty() ? new UserData() : JsonConvert.DeserializeObject<UserData>(data) ?? new UserData();
                userData.Id = id;
                usersData.Add(userData);
            }

            return new UserList {UsersData = usersData};
        }

        public async Task<UserDataResponse> GetUser(string id)
        {
            var baseApiResult = await GetRequest<UserDataResponse>($"User/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UserUpdateResponse> UpdateUser(UserData userData)
        {
            var userUpdateRequest = new UserUpdateRequest
            {
                Id = userData.Id,
                UserData = userData
            };

            var baseApiResult = await PutRequest<UserUpdateResponse>($"User/{userData.Id}", userUpdateRequest);

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