using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Commands;

public record AddFavoriteCurrenciesCommand(
    Guid UserId,
    IReadOnlyCollection<Guid> CurrencyIds
) : IRequest;

public class AddFavoriteCurrency(IFavoriteRepository _favoriteRepository)
    : IRequestHandler<AddFavoriteCurrenciesCommand>
{
    public async Task Handle(AddFavoriteCurrenciesCommand command, CancellationToken ct)
    {
        await _favoriteRepository.AddAsync(
            command.UserId,
            command.CurrencyIds
        );
    }
}