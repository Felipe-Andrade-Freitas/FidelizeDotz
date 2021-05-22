using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.Property(_ => _.Code)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(_ => _.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.HasMany(_ => _.Products)
                .WithOne(_ => _.Category)
                .HasForeignKey(_ => _.CategoryId)
                .IsRequired();

            builder.HasMany(_ => _.Categories)
                .WithOne(_ => _.ParentCategory)
                .HasForeignKey(_ => _.ParentCategoryId)
                .IsRequired(false);
        }
    }
}