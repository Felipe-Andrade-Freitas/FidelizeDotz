using System;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Category;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product
{
    public class ProductResponse
    {
        /// <summary>
        ///     Id of the product
        /// <example>PROD01</example>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Sku code of the product
        /// <example>PROD01</example>
        /// </summary>
        public string SkuCode { get; set; }

        /// <summary>
        ///     Name of the product
        /// <example>Produto 1</example>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Price of the product
        /// <example>120.50</example>
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Price in dots of the product
        /// <example>1250</example>
        /// </summary>
        public int PriceDots { get; set; }

        /// <summary>
        ///     Cashback of the product
        /// <example>120</example>
        /// </summary>
        public int Cashback { get; set; }

        /// <summary>
        ///     Image of the product
        /// <example>data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAA...</example>
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     Category of the product
        /// </summary>
        public CategoryResponse Category { get; set; }
    }
}