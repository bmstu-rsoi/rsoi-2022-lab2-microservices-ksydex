using Microsoft.EntityFrameworkCore;
using PaymentService.Data.Entities;

namespace PaymentService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Payment> Payments => Set<Payment>();
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}