using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
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
            // var authenticationProviderKey = "TestKey";
            // Action<IdentityServerAuthenticationOptions> opt = o =>
            // {
            //     o.Authority = "https://localhost:8001";
            //     o.ApiName = "ApiGateway";
            //     o.SupportedTokens = SupportedTokens.Both;
            //     o.RequireHttpsMetadata = false;
            // };

            // services.AddAuthentication()
            //     .AddJwtBearer(authenticationProviderKey, x =>
            //     {
            //         x.Authority = "https://localhost:8001";
            //         x.Audience = "gateway";
            //         x.RequireHttpsMetadata = false;
            //         x.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidAudiences = new [] {"ApiOne", "ApiTwo"}
            //         };
            //     });

            services.AddControllers();
            services.AddOcelot(Configuration);
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            await app.UseOcelot();
        }
    }
}