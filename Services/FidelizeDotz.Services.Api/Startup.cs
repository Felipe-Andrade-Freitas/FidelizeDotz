using System;
using System.Globalization;
using FidelizeDotz.Services.Api.Configuration;
using FidelizeDotz.Services.Api.CrossCutting.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Swashbuckle.AspNetCore.Filters;

[assembly: ApiController]

namespace FidelizeDotz.Services.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddCors();

            services.AddControllersConfiguration()
                .AddFluentValidation();

            services.AddAppSettingsConfiguration(_configuration);
            services.AddAutomapperConfiguration();
            services.AddDependencyInjectionConfiguration(_configuration);

            services.AddEFMySqlConfiguration(_configuration["ConnectionStrings:FidelizeDotzConnection"]);
            services.AddIdentityConfiguration(_configuration);

            services.AddUserLoggedConfiguration();
            services.AddApiVersioningConfiguration();

            services.AddSwaggerExamples();
            services.AddSwaggerConfiguration(true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger(provider, _configuration["SwaggerConfig:EndpointBase"],
                _configuration["SwaggerConfig:RoutePrefix"]);

            app.UseCors(builder
                => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.MapWhen(context => context.Request.Method.Equals("options", StringComparison.OrdinalIgnoreCase),
                HandleHead);

            // Define FluentValidation Mensages to en-us
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            IdentityModelEventSource.ShowPII = true;
        }

        private static void HandleHead(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Up to head!").ConfigureAwait(false);
            });
        }
    }
}