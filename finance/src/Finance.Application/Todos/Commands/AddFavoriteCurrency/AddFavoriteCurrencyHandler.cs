using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Commands.AddFavoriteCurrency;

public class AddFavoriteCurrencyHandler(IFavoriteRepository _favoriteRepository)
    : IRequestHandler<AddFavoriteCurrencyCommand>
{
    public async Task Handle(AddFavoriteCurrencyCommand command, CancellationToken ct)
    {
        await _favoriteRepository.AddAsync(
            command.UserId,
            command.CurrencyIds
        );
    }
}