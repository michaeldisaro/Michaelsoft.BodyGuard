using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Models;
using Michaelsoft.BodyGuard.Client.Models.Lists;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.Interfaces;
using Michaelsoft.BodyGuard.Common.Models;
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
                var userData = new User();
                
                try
                {
                    userData = JsonConvert.DeserializeObject<User>(data) ?? new User();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                userData.Id = id;
                usersData.Add(userData);
            }

            return new UserList {UsersData = usersData};
        }

        public async Task<User> GetUser(string id)
        {
            var baseApiResult = await GetRequest<UserDataResponse>($"User/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            var userDataResponse = (UserDataResponse) baseApiResult.Response;
            if (!userDataResponse.Success)
                throw new Exception(userDataResponse.Message);

            // parse response
            var userData = userDataResponse.Data.IsNullOrEmpty()
                               ? new User()
                               : JsonConvert.DeserializeObject<User>(userDataResponse.Data) ?? new User();
            userData.Id = id;
            return userData;
        }

        public async Task<UserUpdateResponse> UpdateUser(IUser user)
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