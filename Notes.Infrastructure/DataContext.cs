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
        => await SaveChangesAsync(true, cancellationToken);
}