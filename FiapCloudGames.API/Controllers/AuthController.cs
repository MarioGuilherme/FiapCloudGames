using FiapCloudGames.Application.InputModels;
using FiapCloudGames.Application.Interfaces;
using FiapCloudGames.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService userService) : ControllerBase {
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<AccessTokenViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    public async Task<IActionResult> Login([FromBody] LoginInputModel inputModel) {
        RestResponse<AccessTokenViewModel> restResponse = await this._userService.LoginAsync(inputModel);
        return this.Ok(restResponse);
    }

    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<AccessTokenViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    public async Task<IActionResult> Register([FromBody] RegisterUserInputModel inputModel) {
        RestResponse<AccessTokenViewModel> restResponse = await this._userService.RegisterAsync(inputModel);
        return this.Ok(restResponse);
    }
}
