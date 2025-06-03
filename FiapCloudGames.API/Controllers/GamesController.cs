using FiapCloudGames.Application.ViewModels;
using FiapCloudGames.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase {
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestResponse<dynamic>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(RestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RestResponse))]
    [Authorize(Roles = nameof(UserType.Admin))]
    public Task<IActionResult> Register() => Task.FromResult<IActionResult>(this.Ok()); // Apenas demonstrar erro 403 de permissão
}
