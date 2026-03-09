using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence;

public class FinanceDbContext : DbContext
{
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<FavoriteCurrency> Favorites => Set<FavoriteCurrency>();
    
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currencies");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Code)
                .IsRequired();
            
            entity.HasIndex(x => x.Code)
                .IsUnique();
        });

        modelBuilder.Entity<FavoriteCurrency>(entity =>
        {
            entity.ToTable("favorites");

            // Составной ключ (гарантия уникальности валюты у пользователя)
            entity.HasKey(x => new { x.UserId, x.CurrencyId });

            entity.HasOne(x => x.Currency)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
