using Finance.Application.DTO;
using MediatR;

namespace Finance.Application.Todos.Queries.GetUserCurrencies;

public record GetUserCurrenciesQuery(Guid UserId) : IRequest<List<CurrencyDto>>;
