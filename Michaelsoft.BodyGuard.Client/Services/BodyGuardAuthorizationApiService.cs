using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.HttpModels.Authorization;
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

        public async Task<ManageRoleResponse> AssignRole(string emailAddress,
                                                         string role)
        {
            var manageRoleRequest = new ManageRoleRequest
            {
                EmailAddress = emailAddress,
                Role = role
            };

            var baseApiResult =
                await PutRequest<ManageRoleResponse>("AssignRole", manageRoleRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }
        
        public async Task<ManageRoleResponse> RevokeRole(string emailAddress,
                                                         string role)
        {
            var manageRoleRequest = new ManageRoleRequest
            {
                EmailAddress = emailAddress,
                Role = role
            };

            var baseApiResult =
                await PutRequest<ManageRoleResponse>("RevokeRole", manageRoleRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}