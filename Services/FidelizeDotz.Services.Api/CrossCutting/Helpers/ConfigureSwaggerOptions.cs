using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FidelizeDotz.Services.Api.CrossCutting.Helpers
{
    /// <summary>
    ///     Configures the Swagger generation options.
    /// </summary>
    /// <remarks>
    ///     This allows API versioning to define a Swagger document per API version after the
    ///     <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
    /// </remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration _configuration;
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            var apiVersions = _provider.ApiVersionDescriptions.OrderByDescending(_ => _.ApiVersion.MajorVersion)
                .ThenByDescending(_ => _.ApiVersion.MinorVersion)
                .ToList();

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in apiVersions)
                options.SwaggerDoc(description.GroupName, GetSwaggerInfo(description));
        }

        private OpenApiInfo GetSwaggerInfo(ApiVersionDescription description)
        {
            var apiDescriptions = _configuration.GetSection("SwaggerInfo").Get<IEnumerable<OpenApiInfo>>();

            return apiDescriptions.FirstOrDefault(_ => _.Version == description?.GroupName) ??
                   new OpenApiInfo {Version = description?.GroupName};
        }
    }
}