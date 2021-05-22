using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FidelizeDotz.Services.Api.CrossCutting.Helpers;
using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FidelizeDotz.Services.Api.CrossCutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Incrementa o AddMvc com os seguintes itens:
        ///     1. Filter para GlobalExceptions
        ///     2. Filter para gravação de log dos Inputs e Outputs das requisições
        ///     3. Configurações básicas de serialização de dados
        ///     4. Define o ReturnMessage como retorno padrão para os erros de InvalidModel
        /// </summary>
        /// <param name="includeAdditionalErrorMessageInfo">Parâmentro para incluir uma informação adicional a mensagem de erro</param>
        public static IMvcBuilder AddControllersConfiguration(
            this IServiceCollection services,
            bool includeAdditionalErrorMessageInfo = false)
        {
            return services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    var settings = options.SerializerSettings;
                    settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                })
                .ConfigureApiBehaviorOptions(o =>
                {
                    o.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState.Where(o => o.Value.Errors.Any()).SelectMany(
                            o => o.Value.Errors, (a, b) =>
                                new {a.Key, Error = b}).Select(o => new ErrorMessageInfo(
                            !string.IsNullOrWhiteSpace(o.Error.ErrorMessage)
                                ? o.Error.ErrorMessage
                                : o.Error.Exception?.Message,
                            includeAdditionalErrorMessageInfo ? o.Key : ""));

                        var returnMessage = new ReturnMessage(errors);

                        return new BadRequestObjectResult(returnMessage);
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services, bool includeExampleFilters = false)
        {
            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(options =>
            {
                //Corrige nome dos esquemas para seguir padrão do OpenId 2.0
                options.CustomSchemaIds(type => type.ToString()
                    .Replace("[", "_")
                    .Replace("]", "_")
                    .Replace(",", "-")
                    .Replace("`", "_"));

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // integrate xml comments
                var arquivosHelpAdicionar = Directory.GetFiles(AppContext.BaseDirectory, "*_swagger.xml");
                foreach (var arquivo in arquivosHelpAdicionar)
                    options.IncludeXmlComments(arquivo, true);

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Enter 'Bearer {token}' (don't forget to add 'bearer') into the field below.",
                        Name = "Authorization", Type = SecuritySchemeType.ApiKey
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"},
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                if (includeExampleFilters)
                    options.ExampleFilters();
            });
        }

        public static void AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddUserLoggedConfiguration(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped(options =>
            {
                var httpContext = options.GetService<IHttpContextAccessor>()?.HttpContext;
                return httpContext?.GetUserLogged();
            });
        }
    }
}