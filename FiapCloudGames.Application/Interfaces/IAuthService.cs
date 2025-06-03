using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Application.Interfaces;

public interface IAuthService {
    string GenerateToken(User user);
}
