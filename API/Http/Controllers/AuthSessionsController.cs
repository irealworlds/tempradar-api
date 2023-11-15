
using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Entities;
using API.Http.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthSessionsController(IAuthSessionService authSessionService) : ControllerBase
    {
        [HttpPost("")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] AuthSessionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await authSessionService.Create(request);
            
            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return new CreatedResult();
        }
        
        [HttpDelete("Current")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Authorize]
        public async Task<IActionResult> DeleteCurrentAsync()
        {
            await authSessionService.InvalidateCurrentSession();
            return new NoContentResult();
        }
    }
}
