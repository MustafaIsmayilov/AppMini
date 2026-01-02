using AppMini.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMini.Configurations;

public class DiningTableConfiguration : IEntityTypeConfiguration<DiningTable>
{
    public void Configure(EntityTypeBuilder<DiningTable> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DiningTableNumber)
               .IsRequired();

        builder.Property(x => x.Capacity)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);
        
        builder.HasOne(x => x.Restaurant)
               .WithMany()
               .HasForeignKey(x => x.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}