namespace Finance.Application.DTO;

public record CurrencyDto
{
    public Guid CurrencyId { get; set; }
    public string Name { get; set; } = null!;
    public int Code { get; set; }
    public decimal Rate { get; set; }
}
