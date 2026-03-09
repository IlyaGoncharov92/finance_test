namespace Finance.Application.DTO;

public record CbrCurrencyResponseDto
{
    public int Code { get; set; }
    public string Name { get; set; } = null!;
    public decimal Rate { get; set; }
}