using System.Net.Http;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.BaseClasses;
using Michaelsoft.BodyGuard.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Client.Utilities
{
    public class BodyGuardConfigurationUtility : InjectableServicesBaseStaticClass
    {

        public static void ConfigureCommonSettings()
        {
            using (var scope = Services.CreateScope())
            {
                var bodyGuardClientSettings =
                    scope.ServiceProvider.GetService<IBodyGuardClientSettings>();
                var httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();
                var httpContextAccessor = Services.GetService<IHttpContextAccessor>();
                var options = scope.ServiceProvider.GetService<IOptions<CommonSettings>>();
                var configurationApiService =
                    new BodyGuardConfigurationApiService(bodyGuardClientSettings, httpClientFactory,
                                                         httpContextAccessor, options);
                var task = configurationApiService.ConfigureCommonSettings();
            }

        }

    }

}