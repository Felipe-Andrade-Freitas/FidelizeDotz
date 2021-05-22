using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment));

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Code)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(_ => _.Amount)
                .IsRequired();

            builder.Property(_ => _.Status)
                .HasConversion(new EnumToStringConverter<EPaymentStatus>())
                .IsRequired();

            builder.Property(_ => _.TypePayment)
                .HasConversion(new EnumToStringConverter<ETypePayment>())
                .IsRequired();
        }
    }
}