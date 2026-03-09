namespace Auth.Domain.Entities;

public record User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}
