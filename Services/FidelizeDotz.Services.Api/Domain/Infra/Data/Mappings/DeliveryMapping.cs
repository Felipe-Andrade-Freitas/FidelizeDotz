using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class DeliveryMapping : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable(nameof(Delivery));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Status)
                .HasConversion(new EnumToStringConverter<EDeliveryStatus>())
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(_ => _.TrackingCode)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}