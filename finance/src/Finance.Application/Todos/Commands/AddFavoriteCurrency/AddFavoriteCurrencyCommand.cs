using MediatR;

namespace Finance.Application.Todos.Commands.AddFavoriteCurrency;

public record AddFavoriteCurrencyCommand(
    Guid UserId,
    IReadOnlyCollection<Guid> CurrencyIds
) : IRequest;