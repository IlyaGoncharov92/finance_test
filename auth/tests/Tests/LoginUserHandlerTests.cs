using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth.Application.Todos.Queries;
using Auth.Domain.Entities;
using Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Tests.Help;

namespace Tests;

public class LoginUserHandlerTests(PostgreSqlFixture fixture) : RepositoryTestBase(fixture)
{
    [Fact]
    public async Task Login_ShouldFail_WhenUserNotFound()
    {
        var repo = new UserRepository(Context);
        var hasher = new PasswordHasher<User>();
        var config = JwtTestConfiguration.Create();

        var handler = new LoginUserHandler(repo, hasher, config);

        var result = await handler.Handle(
            new LoginUserQuery("notfound@mail.com", "123"),
            default);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task Login_ShouldFail_WhenPasswordInvalid()
    {
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "user@mail.com",
            PasswordHash = hasher.HashPassword(null!, "correct_password")
        };

        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var repo = new UserRepository(Context);
        var config = JwtTestConfiguration.Create();

        var handler = new LoginUserHandler(repo, hasher, config);

        var result = await handler.Handle(
            new LoginUserQuery("user@mail.com", "wrong_password"),
            default);

        Assert.False(result.IsSuccess);
    }
}