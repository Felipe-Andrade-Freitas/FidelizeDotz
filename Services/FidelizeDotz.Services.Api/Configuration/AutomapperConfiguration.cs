using AutoMapper;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.Domain.Application.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace FidelizeDotz.Services.Api.Configuration
{
    /// <summary>
    ///     Configurações de automapper
    /// </summary>
    public static class AutomapperConfiguration
    {
        public static void AddAutomapperConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IAdapter, AutomapperAdapter>();
            services.AddSingleton(new MapperConfiguration(cfg => { cfg.ConfigureAutomapper(); }).CreateMapper());
        }
    }
}