using System.Xml.Serialization;

namespace Finance.Infrastructure.Models;

[XmlRoot("ValCurs")]
public record CbrResponse
{
    [XmlElement("Valute")]
    public List<CbrCurrencyResponse> Valute { get; set; } = [];
}

public record CbrCurrencyResponse
{
    [XmlElement("NumCode")]
    public int Code { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; } = null!;
    
    [XmlElement("VunitRate")]
    public string Rate { get; set; } = null!;
}