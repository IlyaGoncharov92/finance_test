using Finance.Application.DTO;
using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Commands;

public record UpdateCurrenciesCommand(
    List<CbrCurrencyResponseDto> Currencies
) : IRequest;

public class UpdateCurrenciesHandler1(ICurrencyRepository _currencyRepository)
    : IRequestHandler<UpdateCurrenciesCommand>
{
    public async Task Handle(UpdateCurrenciesCommand command, CancellationToken ct)
    {
        await _currencyRepository.UpsertCurrencies(command.Currencies);
    }
}