using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

namespace FidelizeDotz.Services.Api.CrossCutting.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>Method for serving the generated Swagger middleware.</summary>
        /// <param name="app">Defines a class that provides application settings.</param>
        /// <param name="provider">API information provider.</param>
        /// <param name="endpointBase">Swagger base endpoint.</param>
        /// <param name="routePrefix">Swagger route prefix.</param>
        /// <param name="servers">List url and description of swagger servers. (Key: "URL", Value: "Description")</param>
        public static void UseSwagger(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider,
            string endpointBase = "swagger",
            string routePrefix = "",
            IDictionary<string, string> servers = null)
        {
            endpointBase = endpointBase.TrimStart('/').TrimEnd('/');
            routePrefix = routePrefix.TrimStart('/').TrimEnd('/');
            var regex = endpointBase;
            var routeLastSwaggerJson = "/{0}/swagger.json";
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, request) =>
                {
                    swagger.Servers = new List<OpenApiServer>();
                    if (!routePrefix.IsNullOrEmpty())
                    {
                        swagger.Servers.Add(new OpenApiServer
                        {
                            Url = "/" + routePrefix,
                            Description = "(Gateway Context)"
                        });
                        swagger.Servers.Add(new OpenApiServer
                        {
                            Url = string.Empty,
                            Description = "(Without Gateway)"
                        });
                    }

                    if (servers == null)
                        return;
                    foreach (var server in servers)
                        if (!server.Key.IsNullOrEmpty())
                            swagger.Servers.Add(new OpenApiServer
                            {
                                Url = "/" + server.Key,
                                Description = server.Value
                            });
                });
                c.RouteTemplate = "/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                var list = provider.ApiVersionDescriptions.OrderByDescending(_ => _.ApiVersion.MajorVersion)
                    .ThenByDescending(_ => _.ApiVersion.MinorVersion).ToList();
                routeLastSwaggerJson = string.Format(routeLastSwaggerJson, list.FirstOrDefault().GroupName);
                list.ForEach(description =>
                    c.SwaggerEndpoint("./" + description.GroupName + "/swagger.json", description.GroupName));
            });
            routeLastSwaggerJson =
                routePrefix.IsNullOrEmpty() ? routeLastSwaggerJson : routePrefix + routeLastSwaggerJson;
            var options = new RewriteOptions().AddRedirect(regex, routeLastSwaggerJson);
            app.UseRewriter(options);
        }

        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}