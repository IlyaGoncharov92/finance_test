namespace Auth.Application.Dto;

public record UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; } = null!;
}

public record AuthResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public string? Error { get; init; }
}