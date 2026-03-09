using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using Finance.Application.DTO;
using Finance.Application.Interfaces;
using Finance.Application.Todos.Commands;
using Finance.Infrastructure.Models;
using MediatR;

namespace Finance.Infrastructure.Services;

public class CurrenciesUpdaterService(HttpClient _httpClient, IMediator _mediator) : ICurrencyRatesService
{
    // TODO: В реал проекте вынести в appsettings
    private const string _url = "https://www.cbr.ru/scripts/XML_daily.asp";

    public async Task UpdateRatesAsync(CancellationToken ct)
    {
        var currencies = await GetCurrenciesAsync(ct);
        
        var currenciesDto = currencies.Select(currency => new CbrCurrencyResponseDto
            {
                Code = currency.Code,
                Name = currency.Name,
                Rate = decimal.Parse(
                    currency.Rate,
                    NumberStyles.Float,
                    CultureInfo.GetCultureInfo("ru-RU"))
            })
            .ToList();

        await _mediator.Send(new UpdateCurrenciesCommand(currenciesDto), ct);
    }

    private async Task<List<CbrCurrencyResponse>> GetCurrenciesAsync(CancellationToken ct)
    {
        // Регистрация провайдера для кодировок
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        await using var stream = await _httpClient.GetStreamAsync(_url, ct);

        using var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1251"));

        var serializer = new XmlSerializer(typeof(CbrResponse));
        var response = (CbrResponse) serializer.Deserialize(reader)!;

        return response.Valute;
    }
}
