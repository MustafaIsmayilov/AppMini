using AppMini.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppMini.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.GuestCount)
               .IsRequired();

        builder.Property(x => x.ReservationDate)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("NOW()");

        builder.HasOne(x => x.Restaurant)
               .WithMany()
               .HasForeignKey(x => x.RestaurantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DiningTable)
               .WithMany()
               .HasForeignKey(x => x.DiningTableId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}