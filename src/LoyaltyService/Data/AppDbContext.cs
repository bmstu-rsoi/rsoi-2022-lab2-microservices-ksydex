using LoyaltyService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Loyalty> Loyalties => Set<Loyalty>();
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}