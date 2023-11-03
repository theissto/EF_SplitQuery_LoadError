using EF_SplitQuery_LoadError_Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_SplitQuery_LoadError_Tests;

public class SqliteDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=test.db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ManyItemOneWithoutBackreference>()
            .HasMany(mio => mio.Items)
            .WithMany();

        modelBuilder.Entity<ManyItemOneWithBackreference>()
            .HasMany(mio => mio.Items)
            .WithMany(mit => mit.Items);
    }
}