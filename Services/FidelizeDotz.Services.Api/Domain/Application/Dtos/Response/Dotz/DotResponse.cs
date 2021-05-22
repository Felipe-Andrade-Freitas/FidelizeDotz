using System;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz
{
    public class DotResponse
    {
        /// <summary>
        ///     Created at of the dot
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Value of the dot
        /// </summary>
        public int Quantity { get; set; }
    }
}