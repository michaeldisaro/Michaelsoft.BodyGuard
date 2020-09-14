using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Client.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddBodyGuardApi(this IServiceCollection services,
                                        IConfiguration configuration)
        {
            services.Configure<BodyGuardClientSettings>
                (configuration.GetSection(nameof(BodyGuardClientSettings)));

            services.AddSingleton<IBodyGuardClientSettings>
                (sp => sp.GetRequiredService<IOptions<BodyGuardClientSettings>>().Value);
            
            services.AddHttpClient();
            services.AddSingleton<BodyGuardAuthenticationApiService>();
            services.AddSingleton<BodyGuardAuthorizationApiService>();
        }

    }
}