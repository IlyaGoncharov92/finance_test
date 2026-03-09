using Finance.Application.DTO;

namespace Finance.Application.Interfaces;

public interface ICurrencyRepository
{
    Task UpsertCurrencies(List<CbrCurrencyResponseDto> currencies);
}