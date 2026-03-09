using Finance.Domain.Entities;

namespace Finance.Application.Interfaces;

public interface IFavoriteRepository
{
    Task<List<Currency>> GetUserFavoriteCurrencies(Guid userId);
    Task AddAsync(Guid userId, IReadOnlyCollection<Guid> currencyIds);
}