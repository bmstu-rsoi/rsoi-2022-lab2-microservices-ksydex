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
}