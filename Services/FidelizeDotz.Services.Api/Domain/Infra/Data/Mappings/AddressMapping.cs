using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(nameof(Address));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Street)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(_ => _.Number)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(_ => _.District)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.State)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.Country)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(_ => _.Deliveries)
                .WithOne(_ => _.Address)
                .HasForeignKey(_ => _.AddressId)
                .IsRequired();
        }
    }
}