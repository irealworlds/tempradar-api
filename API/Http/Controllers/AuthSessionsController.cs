
using System.Net;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthSessionsController(SignInManager<ApplicationUser> signInManager) : ControllerBase
    {
        /// <summary>
        /// Terminate the current authentication session by logging the user out.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Current")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await signInManager.SignOutAsync();
            return new NoContentResult();
        }
    }
}
