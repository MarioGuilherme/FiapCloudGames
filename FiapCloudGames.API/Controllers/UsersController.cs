using FiapCloudGames.Application.InputModels;
using FiapCloudGames.Application.Interfaces;
using FiapCloudGames.Application.ViewModels;
using FiapCloudGames.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase {
    private readonly IUserService _userService = userService;

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<IEnumerable<UserViewModel>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize(Roles = nameof(UserType.Admin))]
    public async Task<IActionResult> GetAll() {
        RestResponse<IEnumerable<UserViewModel>> restResponse = await this._userService.GetAllAsync();
        return this.Ok(restResponse);
    }

    [HttpGet("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<UserViewModel>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize(Roles = nameof(UserType.Admin))]
    public async Task<IActionResult> GetById(int userId) {
        RestResponse<UserViewModel> restResponse = await this._userService.GetByIdAsync(userId);
        return this.Ok(restResponse);
    }

    [HttpGet("me")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<UserViewModel>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize]
    public async Task<IActionResult> GetMe() {
        int userId = int.Parse(this.User.Claims.First(c => c.Type == "UserId").Value);
        RestResponse<UserViewModel> restResponse = await this._userService.GetByIdAsync(userId);
        return this.Ok(restResponse);
    }

    [HttpPut("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<UserViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize(Roles = nameof(UserType.Admin))]
    public async Task<IActionResult> UpdateById(int userId, [FromBody] UserInputModel inputModel) {
        RestResponse<UserViewModel> restResponse = await this._userService.UpdateUserAsync(new(userId, inputModel));
        return this.Ok(restResponse);
    }

    [HttpDelete("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize(Roles = nameof(UserType.Admin))]
    public async Task<IActionResult> DeleteById(int userId) {
        await this._userService.DeleteByUserIdAsync(userId);
        return this.NoContent();
    }
}
