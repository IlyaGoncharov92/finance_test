using Finance.Application.Interfaces;
using Finance.Domain.Entities;
using Finance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Repositories;

public class FavoriteRepository(FinanceDbContext _context) : IFavoriteRepository
{
    public async Task<List<Currency>> GetUserFavoriteCurrencies(Guid userId)
    {
        return await _context.Favorites
            .Where(x => x.UserId == userId)
            .Select(x => x.Currency)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Guid userId, IReadOnlyCollection<Guid> currencyIds)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"""
                 INSERT INTO favorites ("user_id", "currency_id")
                 SELECT {userId}, UNNEST({currencyIds})
                 ON CONFLICT ("user_id", "currency_id") DO NOTHING
             """);
    }
}
