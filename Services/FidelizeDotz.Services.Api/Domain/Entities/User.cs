using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<IdentityUserClaim<Guid>> UserClaims { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Dot> Dots { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}