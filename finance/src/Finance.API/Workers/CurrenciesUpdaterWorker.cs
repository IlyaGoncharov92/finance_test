using Finance.Application.Interfaces;

namespace Finance.API.Workers;

public class CurrenciesUpdaterWorker(IServiceProvider _serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope(); 
            var currencyRatesService = scope.ServiceProvider.GetRequiredService<ICurrencyRatesService>();
            
            await currencyRatesService.UpdateRatesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }
}
