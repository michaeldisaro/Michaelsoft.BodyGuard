using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Client.Models.Entities;
using Michaelsoft.BodyGuard.Client.Models.Lists;
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
            var usersData = new List<User>();
            foreach (var (id, data) in usersDataResponse.UsersData)
            {
                var userData = data.IsNullOrEmpty() ? new User() : JsonConvert.DeserializeObject<User>(data) ?? new User();
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

        public async Task<UserUpdateResponse> UpdateUser(User user)
        {
            var userUpdateRequest = new UserUpdateRequest
            {
                Id = user.Id,
                UserData = user
            };

            var baseApiResult = await PutRequest<UserUpdateResponse>($"User/{user.Id}", userUpdateRequest);

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