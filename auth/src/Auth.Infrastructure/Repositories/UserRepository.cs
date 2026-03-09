using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories;

public class UserRepository(AuthDbContext _context) : IUserRepository
{
    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public Task<User?> GetByEmail(string email)
    {
        return _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public Task<bool> ExistsByEmail(string email)
    {
        return _context.Users.AnyAsync(x => x.Email == email);
    }
}