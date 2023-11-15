using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("/Identities/Current")]
public class CurrentIdentityController(IIdentityService identityService) : ControllerBase
{
    /// <summary>
    /// Get information about the current identity.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(IdentityDto), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> ShowAsync()
    {
        var identity = await identityService.GetIdentityByClaimsPrincipal(HttpContext.User);

        if (identity == null)
        {
            return new UnauthorizedResult();
        }
        
        return new JsonResult(identity);
    }
}