using Michaelsoft.BodyGuard.Server.Services;
using Michaelsoft.BodyGuard.Server.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Server.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddEncryptionService(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.Configure<EncryptionSettings>
                (configuration.GetSection(nameof(EncryptionSettings)));

            services.AddSingleton<IEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<EncryptionSettings>>().Value);

            services.AddSingleton<DatabaseEncryptionService>();

        }

        public static void AddUserService(this IServiceCollection services,
                                          IConfiguration configuration)
        {
            services.Configure<UserStoreDatabaseSettings>
                (configuration.GetSection(nameof(UserStoreDatabaseSettings)));

            services.AddSingleton<IUserStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();
        }

        public static void AddTokenService(this IServiceCollection services,
                                           IConfiguration configuration)
        {
            services.Configure<TokenStoreDatabaseSettings>
                (configuration.GetSection(nameof(TokenStoreDatabaseSettings)));

            services.AddSingleton<ITokenStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<TokenStoreDatabaseSettings>>().Value);

            services.AddSingleton<TokenService>();
        }

    }
}