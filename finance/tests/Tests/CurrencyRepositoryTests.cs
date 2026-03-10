using Finance.Application.DTO;
using Finance.Domain.Entities;
using Finance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[Collection(PostgresCollection.Name)]
public class CurrencyRepositoryTests(PostgreSqlFixture fixture) : RepositoryTestBase(fixture)
{
    [Fact]
    public async Task UpsertCurrencies_ShouldInsertNewCurrencies()
    {
        // Arrange
        var currencies = new List<CbrCurrencyResponseDto>
        {
            new() { Code = 840, Name = "USD", Rate = 1 },
            new() { Code = 978, Name = "EUR", Rate = 2 }
        };

        var repository = new CurrencyRepository(Context);

        // Act
        await repository.UpsertCurrencies(currencies);

        // Assert
        var allCurrencies = await Context.Currencies.ToListAsync();
        Assert.Equal(2, allCurrencies.Count);

        Assert.Contains(allCurrencies, x => x.Code == 840 && x.Name == "USD" && x.Rate == 1);
        Assert.Contains(allCurrencies, x => x.Code == 978 && x.Name == "EUR" && x.Rate == 2);
    }

    [Fact]
    public async Task UpsertCurrencies_ShouldUpdateExistingCurrencies()
    {
        // Arrange
        var existingCurrency = new Currency
        {
            Id = Guid.NewGuid(),
            Code = 840,
            Name = "USD",
            Rate = 1
        };
        Context.Currencies.Add(existingCurrency);
        await Context.SaveChangesAsync();

        var repository = new CurrencyRepository(Context);

        var currencies = new List<CbrCurrencyResponseDto>
        {
            new() { Code = 840, Name = "USD", Rate = 1.5m } // обновляем ставку
        };

        // Act
        await repository.UpsertCurrencies(currencies);

        // Assert
        var updatedCurrency = await Context.Currencies.FirstAsync(x => x.Code == 840);
        Assert.Equal(1.5m, updatedCurrency.Rate);
        Assert.Equal("USD", updatedCurrency.Name);
    }

    [Fact]
    public async Task UpsertCurrencies_ShouldInsertAndUpdateMixed()
    {
        // Arrange
        Context.Currencies.Add(new Currency
        {
            Id = Guid.NewGuid(),
            Code = 840,
            Name = "USD",
            Rate = 1
        });
        await Context.SaveChangesAsync();

        var repository = new CurrencyRepository(Context);

        var currencies = new List<CbrCurrencyResponseDto>
        {
            new() { Code = 840, Name = "USD", Rate = 1.2m }, // обновляем
            new() { Code = 978, Name = "EUR", Rate = 2 }     // добавляем
        };

        // Act
        await repository.UpsertCurrencies(currencies);

        // Assert
        var allCurrencies = await Context.Currencies.ToListAsync();
        Assert.Equal(2, allCurrencies.Count);

        var usd = allCurrencies.First(x => x.Code == 840);
        var eur = allCurrencies.First(x => x.Code == 978);

        Assert.Equal(1.2m, usd.Rate);
        Assert.Equal(2m, eur.Rate);
    }
}