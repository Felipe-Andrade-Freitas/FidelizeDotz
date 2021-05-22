using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FidelizeDotz.Services.Api.CrossCutting.Extensions;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Category;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Adapters
{
    /// <summary>
    ///     Configuração de conversão de objetos anêmicos de entrada e saída em objetos de domínio.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AutomapperResolver
    {
        public static void ConfigureAutomapper(this IMapperConfigurationExpression cfg)
        {
            cfg?.CreateMap(typeof(ReturnMessage<>), typeof(ReturnMessage<>)).ReverseMap();
            cfg?.CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ReverseMap();

            cfg?.CreateMap<Dot, DotResponse>();
            cfg?.CreateMap<InsertDotRequest, Dot>();
            cfg?.CreateMap<RescuedDotRequest, Dot>();

            cfg?.CreateMap<InsertCategoryRequest, Category>();
            cfg?.CreateMap<Category, CategoryResponse>();

            cfg?.CreateMap<Product, ProductResponse>();
            cfg?.CreateMap<InsertProductRequest, Product>()
                .ForPath(_ => _.Image, __ => __.MapFrom(___ => ___.ImageUrl));

            cfg?.CreateMap<Order, OrderResponse>();
            cfg?.CreateMap<OrderItem, OrderItemResponse>();
            cfg?.CreateMap<Delivery, DeliveryResponse>();
            cfg?.CreateMap<Payment, PaymentResponse>();

            cfg?.CreateMap<InsertAddressRequest, Address>();
            cfg?.CreateMap<Address, AddressResponse>();
        }
    }
}