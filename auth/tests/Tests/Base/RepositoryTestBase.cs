using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[Collection("postgres")]
public abstract class RepositoryTestBase : IAsyncLifetime
{
    protected readonly PostgreSqlFixture _fixture;
    protected AuthDbContext Context { get; private set; }

    protected RepositoryTestBase(PostgreSqlFixture fixture)
    {
        _fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseNpgsql(_fixture.ConnectionString)
            .UseSnakeCaseNamingConvention()
            .Options;

        Context = new AuthDbContext(options);

        // Полностью пересоздаём базу перед каждым тестовым классом
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        Context.Dispose();
        return Task.CompletedTask;
    }
}