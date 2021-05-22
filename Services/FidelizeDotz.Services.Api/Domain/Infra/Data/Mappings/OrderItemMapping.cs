using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.SkuCode)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(_ => _.Quantity)
                .IsRequired();

            builder.Property(_ => _.UnitPrice)
                .IsRequired();

            builder.Property(_ => _.TotalPrice)
                .IsRequired();
        }
    }
}