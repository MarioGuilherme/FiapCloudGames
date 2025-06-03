using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using static BCrypt.Net.BCrypt;

namespace FiapCloudGames.Application.InputModels;

public class RegisterUserInputModel(string name, string email, string password) {
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;

    public User ToEntity() => new(this.Name, this.Email, HashPassword(this.Password), UserType.User);
}
