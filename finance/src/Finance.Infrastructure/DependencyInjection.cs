using Finance.Application.Interfaces;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        
        services.AddHttpClient<ICurrencyRatesService, CurrenciesUpdaterService>();
        
        return services;
    }
}
