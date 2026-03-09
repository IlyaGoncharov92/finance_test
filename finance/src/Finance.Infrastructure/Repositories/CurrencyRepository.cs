using Finance.Application.DTO;
using Finance.Application.Interfaces;
using Finance.Domain.Entities;
using Finance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Repositories;

public class CurrencyRepository(FinanceDbContext _context) : ICurrencyRepository
{
    public async Task UpsertCurrencies(List<CbrCurrencyResponseDto> currencies)
    {
        var codes = currencies.Select(x => x.Code).ToList();

        // Получаем существующие валюты
        var existingCurrencies = await _context.Currencies
            .Where(x => codes.Contains(x.Code))
            .ToDictionaryAsync(x => x.Code);

        var currenciesToAdd = new List<Currency>();

        foreach (var item in currencies)
        {
            if (existingCurrencies.TryGetValue(item.Code, out var currency))
            {
                // обновляем
                currency.Rate = item.Rate;
            }
            else
            {
                // добавляем
                currenciesToAdd.Add(new Currency
                {
                    Id = Guid.NewGuid(),
                    Code = item.Code,
                    Name = item.Name,
                    Rate = item.Rate
                });
            }
        }

        if (currenciesToAdd.Count > 0)
            await _context.Currencies.AddRangeAsync(currenciesToAdd);

        await _context.SaveChangesAsync();
    }
}
