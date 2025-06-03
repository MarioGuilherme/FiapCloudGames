using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Persistence.Repositories;

public class UserRepository(FiapCloudGamesDbContext context) : IUserRepository {
    private readonly FiapCloudGamesDbContext _context = context;

    public async Task AddAsync(User user) {
        await this._context.Users.AddAsync(user);
        await this._context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user) {
        this._context.Users.Remove(user);
        await this._context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await this._context.Users
        .AsNoTracking()
        .ToListAsync();

    public Task<User?> GetByEmailAsync(string email) => this._context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == email);

    public Task<bool> EmailInUseAsync(string email) => this._context.Users
        .AsNoTracking()
        .AnyAsync(u => u.Email == email);

    public Task<User?> GetByIdAsync(int id) => this._context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.UserId == id);

    public Task<User?> GetByIdTrackingAsync(int id) => this._context.Users.FirstOrDefaultAsync(u => u.UserId == id);

    public async Task UpdateAsync(User user) {
        this._context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
