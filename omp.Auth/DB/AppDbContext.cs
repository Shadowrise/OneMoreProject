using Microsoft.EntityFrameworkCore;

namespace omp.Auth.DB;

public class AppDbContext : DbContext 
{ 
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) 
    { 
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
} 