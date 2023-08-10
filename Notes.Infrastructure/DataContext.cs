using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Note> Notes { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDates();
        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }

    private void UpdateDates()
    {
        var entities = ChangeTracker.Entries<BaseEntity>();
        var now = DateTime.UtcNow;

        foreach (var entry in entities)
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreateAt = now;
            
            entry.Entity.UpdatedAt = now;
        }
    }
}