using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Domain.Entities;

public class User {
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public UserType UserType { get; private set; }
    public string Password { get; private set; }

    public User(int userId, string name, string email, string password, UserType userType) {
        this.UserId = userId;
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.UserType = userType;
    }

    public User(string name, string email, string password, UserType userType) {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.UserType = userType;
    }

    public void Update(string name, string email, string password) {
        this.Name = name;
        this.Email = email;
        this.Password = password;
    }
}
