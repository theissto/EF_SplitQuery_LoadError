using EF_SplitQuery_LoadError_Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_SplitQuery_LoadError_Tests;

public class PostgreSqlTests
{
    [SetUp]
    public void SetUp()
    {
        var dbContext = new PostgreSqlDbContext();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    [Test]
    public async Task _01_PostgreSQL_ExplicitLoad_OnManyToManyCollection_WithoutBackreference_ThrowsNoError()
    {
        var manyItemOne = new ManyItemOneWithoutBackreference()
        {
            Items = new()
            {
                new ()
            }
        };
        
        await using (var postgresDbContext = new PostgreSqlDbContext())
        {
            postgresDbContext.Add(manyItemOne);
            await postgresDbContext.SaveChangesAsync();
        }

        await using (var postgresDbContext = new PostgreSqlDbContext())
        {
            var manyItemOneFromDb = await postgresDbContext.Set<ManyItemOneWithoutBackreference>().SingleAsync(i => i.Id == manyItemOne.Id);
            var collectionQuery = postgresDbContext.Entry(manyItemOneFromDb)
                .Collection(i => i.Items)
                .Query();

            var normalQueryResult = await collectionQuery.ToListAsync();
            
            Assert.That(normalQueryResult, Has.Count.EqualTo(1));
            Assert.DoesNotThrowAsync(async () => await collectionQuery.AsSplitQuery().ToListAsync());

            var splitQueryResult = await collectionQuery.AsSplitQuery().ToListAsync();
            Assert.That(splitQueryResult, Has.Count.EqualTo(1));
        }
    }
    
    [Test]
    public async Task _02_PostgreSQL_ExplicitLoad_OnManyToManyCollection_WithBackreference_ThrowsNoError()
    {
        var manyItemOne = new ManyItemOneWithBackreference()
        {
            Items = new()
            {
                new ()
            }
        };
        
        await using (var postgresDbContext = new PostgreSqlDbContext())
        {
            postgresDbContext.Add(manyItemOne);
            await postgresDbContext.SaveChangesAsync();
        }

        await using (var postgresDbContext = new PostgreSqlDbContext())
        {
            var manyItemOneFromDb = await postgresDbContext.Set<ManyItemOneWithBackreference>()
                .SingleAsync(i => i.Id == manyItemOne.Id);
            
            var collectionQuery = postgresDbContext.Entry(manyItemOneFromDb)
                .Collection(i => i.Items)
                .Query();

            var normalQueryResult = await collectionQuery.ToListAsync();
            
            Assert.That(normalQueryResult, Has.Count.EqualTo(1));
            Assert.DoesNotThrowAsync(async () => await collectionQuery.AsSplitQuery().ToListAsync());

            var splitQueryResult = await collectionQuery.AsSplitQuery().ToListAsync();
            Assert.That(splitQueryResult, Has.Count.EqualTo(1));
        }
    }
}