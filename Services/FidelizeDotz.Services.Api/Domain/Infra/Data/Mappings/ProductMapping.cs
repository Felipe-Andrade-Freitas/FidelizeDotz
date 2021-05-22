using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.SkuCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(_ => _.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(_ => _.Price)
                .IsRequired();

            builder.Property(_ => _.PriceDots)
                .IsRequired();

            builder.Property(_ => _.Cashback)
                .IsRequired();

            builder.Property(_ => _.Image)
                .IsRequired();

            builder.HasMany(_ => _.OrderItems)
                .WithOne(_ => _.Product)
                .HasForeignKey(_ => _.ProductId);
        }
    }
}