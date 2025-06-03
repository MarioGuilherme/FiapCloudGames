using FiapCloudGames.Application.InputModels;
using FiapCloudGames.Application.Interfaces;
using FiapCloudGames.Application.ViewModels;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Exceptions;
using FiapCloudGames.Domain.Repositories;
using static BCrypt.Net.BCrypt;

namespace FiapCloudGames.Application.Services;

public class UserService(IUserRepository userRepository, IAuthService authService) : IUserService {
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthService _authService = authService;

    public async Task<RestResponse> DeleteByUserIdAsync(int userId) {
        User? user = await this._userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        await this._userRepository.DeleteAsync(user);
        return RestResponse.Success();
    }

    public async Task<RestResponse<IEnumerable<UserViewModel>>> GetAllAsync() {
        IEnumerable<User> users = await this._userRepository.GetAllAsync();
        return RestResponse<IEnumerable<UserViewModel>>.Success(users.Select(UserViewModel.FromEntity));
    }

    public async Task<RestResponse<UserViewModel>> GetByIdAsync(int userId) {
        User? user = await this._userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        return RestResponse<UserViewModel>.Success(UserViewModel.FromEntity(user));
    }

    public async Task<RestResponse<AccessTokenViewModel>> LoginAsync(LoginInputModel inputModel) {
        User? user = await this._userRepository.GetByEmailAsync(inputModel.Email) ?? throw new UserNotFoundException();

        if (!Verify(inputModel.Password, user.Password)) throw new UserNotFoundException();

        string accessToken = this._authService.GenerateToken(user);
        return RestResponse<AccessTokenViewModel>.Success(new(accessToken));
    }

    public async Task<RestResponse<AccessTokenViewModel>> RegisterAsync(RegisterUserInputModel inputModel) {
        if (await this._userRepository.EmailInUseAsync(inputModel.Email)) throw new EmailAlreadyInUseException();

        User newUser = inputModel.ToEntity();
        await this._userRepository.AddAsync(newUser);

        string accessToken = this._authService.GenerateToken(newUser);
        return RestResponse<AccessTokenViewModel>.Success(new(accessToken));
    }

    public async Task<RestResponse<UserViewModel>> UpdateUserAsync(UpdateUserInputModel inputModel) {
        User? user = await this._userRepository.GetByIdTrackingAsync(inputModel.UserId) ?? throw new UserNotFoundException();
        if (await this._userRepository.EmailInUseAsync(inputModel.Email))
            throw new EmailAlreadyInUseException();

        user.Update(inputModel.Name, inputModel.Email, HashPassword(inputModel.Password));

        await this._userRepository.UpdateAsync(user);
        return RestResponse<UserViewModel>.Success(UserViewModel.FromEntity(user));
    }
}