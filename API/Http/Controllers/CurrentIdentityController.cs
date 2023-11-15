using API.Domain.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("/Identities/Current")]
public class CurrentIdentityController(IIdentityService identityService) : ControllerBase
{
    [Authorize]
    [HttpGet]
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