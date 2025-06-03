namespace FiapCloudGames.Application.InputModels;

public class UpdateUserInputModel(int userId, UserInputModel userInputModel) : UserInputModel(
    userInputModel.Name,
    userInputModel.Email,
    userInputModel.Password
) {
    public int UserId { get; private set; } = userId;
}
