using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.HttpModels.Authentication;
using Michaelsoft.BodyGuard.Common.Models;
using Michaelsoft.BodyGuard.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardConfigurationApiService : BodyGuardBaseApiService
    {

        private readonly CommonSettings _commonSettings;

        public BodyGuardConfigurationApiService(IBodyGuardClientSettings settings,
                                                IHttpClientFactory httpClientFactory,
                                                IHttpContextAccessor httpContextAccessor,
                                                IOptions<CommonSettings> commonSettings) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
            _commonSettings = commonSettings.Value;
        }

        public async Task ConfigureCommonSettings()
        {
            await PostRequest<UserCreateResponse>("ConfigureCommonSettings", _commonSettings);
        }

    }
}