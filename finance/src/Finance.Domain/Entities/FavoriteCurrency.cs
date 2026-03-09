namespace Finance.Domain.Entities;

public record FavoriteCurrency
{
    public Guid UserId { get; set; }
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
}
