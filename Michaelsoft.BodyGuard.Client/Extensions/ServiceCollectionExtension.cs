using System;
using System.Collections.Generic;
using System.Globalization;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.JsonOptions;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Client.Services;
using Michaelsoft.BodyGuard.Client.Settings;
using Michaelsoft.BodyGuard.Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Client.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddBodyGuard(this IServiceCollection services,
                                        IConfiguration configuration)
        {
            services.Configure<CommonSettings>(configuration.GetSection("CommonSettings"));

            services.Configure<BodyGuardClientSettings>
                (configuration.GetSection(nameof(BodyGuardClientSettings)));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("it-IT"),
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddJsonLocalization(options =>
            {
                options.LocalizationMode = LocalizationMode.I18n;
                options.UseBaseName = false;
                options.IsAbsolutePath = false;
                options.ResourcesPath = "Resources/";
                options.CacheDuration = TimeSpan.FromMinutes(60 * 24);
                options.SupportedCultureInfos = new HashSet<CultureInfo>
                {
                    new CultureInfo("it-IT"),
                    new CultureInfo("en-US")
                };
            });

            services.AddSingleton<IBodyGuardClientSettings>
                (sp => sp.GetRequiredService<IOptions<BodyGuardClientSettings>>().Value);

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddSingleton<IBodyGuardAuthenticationApiService, BodyGuardAuthenticationApiService>();
            services.AddSingleton<IBodyGuardUserApiService, BodyGuardUserApiService>();
            services.AddSingleton<IBodyGuardAuthorizationApiService, BodyGuardAuthorizationApiService>();

            services.AddRazorPages()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

    }
}