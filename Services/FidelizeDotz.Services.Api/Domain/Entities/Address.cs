using System;
using System.Collections.Generic;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Address : EntityBase
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
    }
}