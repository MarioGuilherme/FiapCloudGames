using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FiapCloudGames.Infrastructure.Persistence;

public class FiapCloudGamesDbContext(DbContextOptions<FiapCloudGamesDbContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
