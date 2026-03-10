using Auth.Domain.Entities;
using Auth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[Collection(PostgresCollection.Name)]
public class UserRepositoryTests(PostgreSqlFixture fixture) : RepositoryTestBase(fixture)
{
    [Fact]
    public async Task Add_ShouldInsertUser()
    {
        var repo = new UserRepository(Context);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@mail.com",
            Name = "Test",
            PasswordHash = "hash"
        };

        await repo.Add(user);

        var dbUser = await Context.Users.FirstOrDefaultAsync(x => x.Email == "test@mail.com");

        Assert.NotNull(dbUser);
        Assert.Equal(user.Id, dbUser!.Id);
    }

    [Fact]
    public async Task ExistsByEmail_ShouldReturnTrue_WhenUserExists()
    {
        var repo = new UserRepository(Context);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "exists@mail.com",
            PasswordHash = "hash"
        };

        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var exists = await repo.ExistsByEmail("exists@mail.com");

        Assert.True(exists);
    }

    [Fact]
    public async Task GetByEmail_ShouldReturnUser()
    {
        var repo = new UserRepository(Context);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "get@mail.com",
            PasswordHash = "hash"
        };

        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var result = await repo.GetByEmail("get@mail.com");

        Assert.NotNull(result);
        Assert.Equal(user.Email, result!.Email);
    }
}