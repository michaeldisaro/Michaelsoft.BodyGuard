using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.Encryption;
using Michaelsoft.BodyGuard.Server.Services;
using Michaelsoft.BodyGuard.Server.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Michaelsoft.BodyGuard.Server
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EncryptionSettings>
                (Configuration.GetSection(nameof(EncryptionSettings)));

            services.AddSingleton<IEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<EncryptionSettings>>().Value);


            services.Configure<UserStoreDatabaseSettings>
                (Configuration.GetSection(nameof(UserStoreDatabaseSettings)));

            services.AddSingleton<IUserStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

            services.AddSingleton<DatabaseEncryptionService>();
            services.AddSingleton<UserService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }
}