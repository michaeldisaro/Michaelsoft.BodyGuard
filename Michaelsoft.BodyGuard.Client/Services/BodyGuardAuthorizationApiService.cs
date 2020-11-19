using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Http;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardAuthorizationApiService : BodyGuardBaseApiService, IBodyGuardAuthorizationApiService
    {

        public BodyGuardAuthorizationApiService(IBodyGuardClientSettings settings,
                                                IHttpClientFactory httpClientFactory,
                                                IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<ManageRoleResponse> AssignRole(string userId,
                                                         string role)
        {
            var manageRoleRequest = new ManageRoleRequest
            {
                UserId = userId,
                Role = role
            };

            var baseApiResult =
                await PostRequest<ValidateRecoveryResponse>("AssignRole", manageRoleRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }
        
        public async Task<ManageRoleResponse> RevokeRole(string userId,
                                                         string role)
        {
            var manageRoleRequest = new ManageRoleRequest
            {
                UserId = userId,
                Role = role
            };

            var baseApiResult =
                await PostRequest<ValidateRecoveryResponse>("RevokeRole", manageRoleRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}