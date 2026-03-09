namespace Finance.Domain.Entities;

public record Currency
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Code { get; set; }
    public decimal Rate { get; set; }
    public ICollection<FavoriteCurrency> Favorites { get; set; } = [];
}
