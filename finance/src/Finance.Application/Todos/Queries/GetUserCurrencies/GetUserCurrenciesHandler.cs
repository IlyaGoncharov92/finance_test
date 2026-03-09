using Finance.Application.DTO;
using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Queries.GetUserCurrencies;

public class GetUserCurrenciesHandler(IFavoriteRepository _favoriteRepository)
    : IRequestHandler<GetUserCurrenciesQuery, List<CurrencyDto>>
{
    public async Task<List<CurrencyDto>> Handle(GetUserCurrenciesQuery request, CancellationToken ct)
    {
        var currencies = await _favoriteRepository.GetUserFavoriteCurrencies(request.UserId);

        var result = currencies
            .Select(x => new CurrencyDto
            {
                CurrencyId = x.Id,
                Code = x.Code,
                Name = x.Name,
                Rate = 1.0m
            })
            .ToList();

        return result;
    }
}