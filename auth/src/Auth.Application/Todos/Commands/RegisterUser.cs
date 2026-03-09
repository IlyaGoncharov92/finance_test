using Auth.Application.Dto;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Todos.Commands;

public record RegisterUserCommand(string Email, string? Name, string Password) : IRequest<AuthResult<UserDto>>;

public class RegisterUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    : IRequestHandler<RegisterUserCommand, AuthResult<UserDto>>
{
    public async Task<AuthResult<UserDto>> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        if (await userRepository.ExistsByEmail(request.Email))
            return new AuthResult<UserDto> { IsSuccess = false, Error = "User already exists" };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        await userRepository.Add(user);

        return new AuthResult<UserDto>
        {
            IsSuccess = true,
            Value = new UserDto { Id = user.Id, Name = user.Name, Email = user.Email }
        };
    }
}
