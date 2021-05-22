using FidelizeDotz.Services.Api.Domain.Application.Dtos.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FidelizeDotz.Services.Api.Configuration
{
    /// <summary>
    ///     Responsável por injetar as propriedades do appSettings
    /// </summary>
    public static class AppSettingsConfiguration
    {
        public static void AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(service => configuration.GetSection(nameof(AppSettings)).Get<AppSettings>());
        }
    }
}