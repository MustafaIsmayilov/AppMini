using AppMini.Configurations;
using AppMini.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppMini.Data;

public class AppMiniDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MiniApp;Username=postgres;Password=20062006;");
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<DiningTable> DiningTables { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}
