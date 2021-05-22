using System;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Dot : EntityBase
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}