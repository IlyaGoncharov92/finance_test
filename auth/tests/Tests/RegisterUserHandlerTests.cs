using Auth.Application.Todos.Commands;
using Auth.Domain.Entities;
using Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[Collection(PostgresCollection.Name)]
public class RegisterUserHandlerTests(PostgreSqlFixture fixture) : RepositoryTestBase(fixture)
{
    [Fact]
    public async Task Register_ShouldCreateUser()
    {
        var repo = new UserRepository(Context);
        var hasher = new PasswordHasher<User>();

        var handler = new RegisterUserHandler(repo, hasher);

        var command = new RegisterUserCommand(
            "new@mail.com",
            "John",
            "123456"
        );

        var result = await handler.Handle(command, default);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        var dbUser = await Context.Users.FirstOrDefaultAsync(x => x.Email == "new@mail.com");

        Assert.NotNull(dbUser);
    }

    [Fact]
    public async Task Register_ShouldFail_WhenUserExists()
    {
        Context.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Email = "exists@mail.com",
            PasswordHash = "hash"
        });

        await Context.SaveChangesAsync();

        var repo = new UserRepository(Context);
        var hasher = new PasswordHasher<User>();

        var handler = new RegisterUserHandler(repo, hasher);

        var command = new RegisterUserCommand(
            "exists@mail.com",
            "John",
            "123456"
        );

        var result = await handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal("User already exists", result.Error);
    }
}