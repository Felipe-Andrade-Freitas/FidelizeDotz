using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class DotMapping : IEntityTypeConfiguration<Dot>
    {
        public void Configure(EntityTypeBuilder<Dot> builder)
        {
            builder.ToTable(nameof(Dot));

            builder.HasKey(_ => _.Id);
            
            builder.Property(_ => _.Quantity)
                .IsRequired();
        }
    }
}