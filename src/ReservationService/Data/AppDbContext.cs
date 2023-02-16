using Microsoft.EntityFrameworkCore;
using ReservationService.Data.Entities;

namespace ReservationService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .HasData(new List<Hotel>
            {
                new Hotel
                {
                    Id = 1,
                    HotelUid = Guid.Parse("049161bb-badd-4fa8-9d90-87c9a82b0668"),
                    Name = "Ararat Park Hyatt Moscow",
                    Country = "Россия",
                    City = "Москва",
                    Address = "Неглинная ул., 4",
                    Stars = 5,
                    Price = 10000
                }
            });
    }
}