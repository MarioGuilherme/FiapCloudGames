namespace FiapCloudGames.Application.InputModels;

public class UserInputModel(string name, string email, string password) {
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
}
