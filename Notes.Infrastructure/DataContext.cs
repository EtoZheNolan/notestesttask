using Humanizer;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }
    
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
        ApplySnakeCaseNamingConvention(modelBuilder);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        base.OnModelCreating(modelBuilder);
    }

    private void ApplySnakeCaseNamingConvention(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().Underscore());
            
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().Underscore());
            
            foreach (var key in entity.GetKeys())
                key.SetName(key.GetName().Underscore());
            
            foreach (var key in entity.GetForeignKeys())
                key.SetConstraintName(key.GetConstraintName().Underscore());
            
            foreach (var index in entity.GetIndexes())
                index.SetDatabaseName(index.GetDatabaseName().Underscore());
        }
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