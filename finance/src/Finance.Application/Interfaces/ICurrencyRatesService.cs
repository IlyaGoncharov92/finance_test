
namespace Finance.Application.Interfaces;

public interface ICurrencyRatesService
{
    Task UpdateRatesAsync(CancellationToken ct);
}