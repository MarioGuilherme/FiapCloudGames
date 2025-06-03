using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Application.ViewModels;

public record UserViewModel(int Id, string Name, string Email, string UserType) {
    public static UserViewModel FromEntity(User user) => new(
        user.UserId,
        user.Name,
        user.Email,
        user.UserType.ToString()
    );
}
