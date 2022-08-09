using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class IceCreamConfiguration : IEntityTypeConfiguration<IceCream>
    {
        public void Configure(EntityTypeBuilder<IceCream> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.Flavour)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(i => i.Color)
                .HasMaxLength(30);

            builder
                .Property(i => i.Price)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder
                .Property(i => i.WeightInGrams)
                .IsRequired();
        }
    }
}
