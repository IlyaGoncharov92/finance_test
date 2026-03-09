using Finance.Application.DTO;
using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Queries;

public record GetFavoriteCurrenciesQuery(Guid UserId) : IRequest<List<CurrencyDto>>;

public class GetFavoriteCurrenciesHandler(IFavoriteRepository _favoriteRepository)
    : IRequestHandler<GetFavoriteCurrenciesQuery, List<CurrencyDto>>
{
    public async Task<List<CurrencyDto>> Handle(GetFavoriteCurrenciesQuery request, CancellationToken ct)
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
