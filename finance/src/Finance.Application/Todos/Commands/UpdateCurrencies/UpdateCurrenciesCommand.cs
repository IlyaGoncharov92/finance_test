using Finance.Application.DTO;
using MediatR;

namespace Finance.Application.Todos.Commands.UpdateCurrencies;

public record UpdateCurrenciesCommand(
    List<CbrCurrencyResponseDto> Currencies
) : IRequest;