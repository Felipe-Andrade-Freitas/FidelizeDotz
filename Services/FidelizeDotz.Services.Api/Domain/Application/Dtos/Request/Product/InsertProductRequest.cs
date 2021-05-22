using System;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product
{
    public class InsertProductRequest
    {
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
        /// <example>https://minhaimagem.jpg</example>
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        ///     Id of the Category of the product
        /// <example>d58d81ad-3d79-4fdf-b7ae-4a501262b5cc</example>
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}