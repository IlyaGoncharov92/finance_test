using Finance.Domain.Entities;
using Finance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[Collection(PostgresCollection.Name)]
public class FavoriteRepositoryTests(PostgreSqlFixture fixture) : RepositoryTestBase(fixture)
{
    [Fact]
    public async Task AddAsync_ShouldInsertFavorites()
    {
        var repository = new FavoriteRepository(Context);

        var userId = Guid.NewGuid();
        var currencyIds = new[]
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        foreach (var currencyId in currencyIds)
        {
            Context.Currencies.Add(new Currency { Id = currencyId, Name = "test", Code = Random.Shared.Next(1, 999), Rate = 1 });
        }
        await Context.SaveChangesAsync();

        // Act
        await repository.AddAsync(userId, currencyIds);

        // Assert
        var favorites = await Context.Favorites.ToListAsync();
        Assert.Equal(3, favorites.Count);
        Assert.All(favorites, x => Assert.Equal(userId, x.UserId));
    }

    [Fact]
    public async Task AddAsync_ShouldNotInsertDuplicateFavorites()
    {
        var repository = new FavoriteRepository(Context);

        var userId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();

        Context.Currencies.Add(new Currency { Id = currencyId, Name = "USD", Code = 840, Rate = 1 });
        await Context.SaveChangesAsync();

        // Act
        await repository.AddAsync(userId, [currencyId]);
        await repository.AddAsync(userId, [currencyId]); // повторная попытка вставки

        // Assert
        var favorites = await Context.Favorites.ToListAsync();
        Assert.Single(favorites);
        Assert.Equal(userId, favorites[0].UserId);
        Assert.Equal(currencyId, favorites[0].CurrencyId);
    }
}