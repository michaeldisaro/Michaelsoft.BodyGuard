using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthenticationApiService : BodyGuardBaseApiService, IBodyGuardAuthenticationApiService
    {

        public BodyGuardAuthenticationApiService(IBodyGuardClientSettings settings,
                                                 IHttpClientFactory httpClientFactory,
                                                 IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
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

        public async Task<UserLoginResponse> Login(string email,
                                                   string password)
        {
            var userLoginRequest = new UserLoginRequest
            {
                EmailAddress = email,
                Password = password
            };

            var baseApiResult = await PostRequest<UserLoginResponse>("Login", userLoginRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }
        
        public async Task<UserLogoutResponse> Logout()
        {
            var baseApiResult = await PostRequest<UserLogoutResponse>("Logout");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}