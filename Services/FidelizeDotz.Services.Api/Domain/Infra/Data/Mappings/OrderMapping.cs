using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Code)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(_ => _.Status)
                .HasConversion(new EnumToStringConverter<EOrderStatus>())
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(_ => _.OrderItems)
                .WithOne(_ => _.Order)
                .HasForeignKey(_ => _.OrderId)
                .IsRequired();

            builder.HasMany(_ => _.Deliveries)
                .WithOne(_ => _.Order)
                .HasForeignKey(_ => _.OrderId)
                .IsRequired();

            builder.HasMany(_ => _.Payments)
                .WithOne(_ => _.Order)
                .HasForeignKey(_ => _.OrderId)
                .IsRequired();
        }
    }
}