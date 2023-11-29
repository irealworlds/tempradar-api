using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Authorization.Requirements;

namespace API.Http.Controllers
{
    [ApiController]
    [Route("PinnedCities/{cityId:guid}/Weather")]
    public class PinnedCitiesWeatherController(IPinnedCityService pinnedCityService, IPinnedCityWeatherService pinnedCityWeatherService, IAuthorizationService authorizationService) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PinnedCityWeatherDetailsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPinnedCityWeatherDetails(Guid cityId)
        {
            // Fetch the city details
            var city = await pinnedCityService.GetByIdAsync(cityId);

            // Make sure a city with that ID actually exists
            if (city == null)
            {
                return NotFound();
            }

            // Perform authorization
            var authorizationResult = await authorizationService.AuthorizeAsync(HttpContext.User, city, Operations.Read);

            if (!authorizationResult.Succeeded)
            {
                if (User.Identity is { IsAuthenticated: true })
                {
                    return new ForbidResult();
                }

                return new ChallengeResult();
            }
            
            // Get the temperature
            try
            {
                if (cityId == Guid.Empty)
                {
                    return BadRequest("Invalid id parameter");
                }

                var pinnedCityWeatherDetails = await pinnedCityWeatherService.GetWeatherDetailsAsync(cityId);

                if (pinnedCityWeatherDetails == null)
                {
                    return NotFound();
                }

                return Ok(pinnedCityWeatherDetails);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, "An error occurred while getting the pinned city weather details: " + ex.Message);
            }
        }

        [HttpGet("History")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PinnedCityWeatherDetailsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPinnedCityWeatherHistory(Guid cityId)
        {
            // Fetch the city details
            var city = await pinnedCityService.GetByIdAsync(cityId);

            // Make sure a city with that ID actually exists
            if (city == null)
            {
                return NotFound();
            }

            // Perform authorization
            var authorizationResult = await authorizationService.AuthorizeAsync(HttpContext.User, city, Operations.Read);

            if (!authorizationResult.Succeeded)
            {
                if (User.Identity is { IsAuthenticated: true })
                {
                    return new ForbidResult();
                }

                return new ChallengeResult();
            }
            
            // Get the temperature history
            try
            {
                if (cityId == Guid.Empty)
                {
                    return BadRequest("Invalid id parameter");
                }

                var pinnedCityDailyTemperatures = await pinnedCityWeatherService.GetWeatherHistoryAsync(cityId);

                if (pinnedCityDailyTemperatures == null)
                {
                    return NotFound();
                }

                return Ok(pinnedCityDailyTemperatures);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, "An error occurred while getting the pinned city weather history: " + ex.Message);
            }
        }
    }
}
