using LoyaltyService.Data.Entities;
using LoyaltyService.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Loyalty> Loyalties => Set<Loyalty>();
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loyalty>()
            .HasData(new List<Loyalty>
            {
                new Loyalty
                {
                    Id = 1,
                    UserName = "Test max",
                    ReservationCount = 25,
                    Status = LoyaltyHelpers.Types.Gold,
                    Discount = 10
                }
            });
    }
}