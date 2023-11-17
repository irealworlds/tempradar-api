using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentitiesController(IIdentityService identityService) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ActionName(nameof(CreateAsync))]
    [ProducesResponseType(typeof(IdentityDto), (int) HttpStatusCode.Created)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] IdentityCreationDataDto requestDto)
    {
        // Create the identity
        IdentityDto user;
        try
        {
            user = await identityService.CreateIdentity(requestDto);
        }
        catch (ArgumentException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return BadRequest(ModelState);
        }
        
        // Return a 201 Created response
        return CreatedAtAction(nameof(ShowAsync), user);
    }
    
    [Authorize]
    [HttpGet("Current")]
    [ActionName(nameof(ShowAsync))]
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