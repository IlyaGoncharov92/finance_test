using Finance.Application.Interfaces;
using MediatR;

namespace Finance.Application.Todos.Commands.UpdateCurrencies;

public class UpdateCurrenciesHandler(ICurrencyRepository _currencyRepository)
    : IRequestHandler<UpdateCurrenciesCommand>
{
    public async Task Handle(UpdateCurrenciesCommand command, CancellationToken ct)
    {
        await _currencyRepository.UpsertCurrencies(command.Currencies);
    }
}