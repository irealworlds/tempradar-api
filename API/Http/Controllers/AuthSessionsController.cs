using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthSessionsController(IAuthSessionService authSessionService) : ControllerBase
{
    [HttpPost("")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] AuthSessionCreationDataDto requestDto)
    {
        await authSessionService.Create(requestDto);

        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        return new CreatedResult();
    }

    [Authorize]
    [HttpDelete("Current")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> DeleteCurrentAsync()
    {
        await authSessionService.InvalidateCurrentSession();
        return new NoContentResult();
    }
}