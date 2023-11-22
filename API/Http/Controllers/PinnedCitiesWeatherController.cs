using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Http.Controllers
{
    [ApiController]
    public class PinnedCitiesWeatherController(IPinnedCityWeatherService pinnedCityWeatherService) : ControllerBase
    {
        [HttpGet("PinnedCities/{id:guid}/Weather")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PinnedCityWeatherDetailsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPinnedCityWeatherDetails(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid id parameter");
                }

                var pinnedCityWeatherDetails = await pinnedCityWeatherService.GetWeatherDetailsAsync(id);

                if (pinnedCityWeatherDetails == null)
                {
                    return NotFound();
                }

                return Ok(pinnedCityWeatherDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the pinned city weather details: " + ex.Message);
            }
        }

        [HttpGet("PinnedCities/{id:guid}/Weather/History")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PinnedCityWeatherDetailsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPinnedCityWeatherHistory(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid id parameter");
                }

                var pinnedCityDailyTemperatures = await pinnedCityWeatherService.GetWeatherHistoryAsync(id);

                if (pinnedCityDailyTemperatures == null)
                {
                    return NotFound();
                }

                return Ok(pinnedCityDailyTemperatures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the pinned city weather history: " + ex.Message);
            }
        }
    }
}
