using EF_SplitQuery_LoadError_Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_SplitQuery_LoadError_Tests;

public class PostgreSqlDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            @"User ID=SplitQueryTestUser;Password=postgres;Server=localhost;Port=5432;Database=EF_SplitQuery_LoadError_Tests;Pooling=true;Include Error Detail=true;Command Timeout=0");
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