using System;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FidelizeDotz.Services.Api.Domain.Application.Services;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Application.Validations;
using FidelizeDotz.Services.Api.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FidelizeDotz.Services.Api.Configuration
{
    /// <summary>
    ///     Configurações de injeção de dependência
    /// </summary>
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddIoCValidators();
            services.AddSvcAgents(configuration);
            services.AddIoCRepositories();

            services.AddIoCApplicationServices();
        }

        private static void AddIoCValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<InsertAddressRequest>, InsertAddressRequestValidation>();
            services.AddScoped<IValidator<InsertCategoryRequest>, InsertCategoryRequestValidation>();
            services.AddScoped<IValidator<InsertDotRequest>, InsertDotRequestValidation>();
            services.AddScoped<IValidator<InsertProductRequest>, InsertProductRequestValidation>();
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidation>();
            services.AddScoped<IValidator<LoginUserRequest>, LoginUserRequestValidation>();
            services.AddScoped<IValidator<RescuedDotRequest>, RescuedDotRequestValidation>();
        }

        private static void AddIoCApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDotzService, DotzService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        private static void AddIoCRepositories(this IServiceCollection services)
        {
        }

        private static void AddSvcAgents(this IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}