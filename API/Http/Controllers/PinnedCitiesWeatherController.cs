using System.Net;
using API.Authorization.Requirements;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("PinnedCities/{cityId:guid}/Weather")]
public class PinnedCitiesWeatherController(IPinnedCityService pinnedCityService,
    IPinnedCityWeatherService pinnedCityWeatherService, IAuthorizationService authorizationService) : ControllerBase
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
        if (city == null) return this.NotFound();

        // Perform authorization
        var authorizationResult =
            await authorizationService.AuthorizeAsync(this.HttpContext.User, city, Operations.Read);

        if (!authorizationResult.Succeeded)
        {
            if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

            return new ChallengeResult();
        }

        // Get the temperature
        try
        {
            if (cityId == Guid.Empty) return this.BadRequest("Invalid id parameter");

            var pinnedCityWeatherDetails = await pinnedCityWeatherService.GetWeatherDetailsAsync(cityId);

            if (pinnedCityWeatherDetails == null) return this.NotFound();

            return this.Ok(pinnedCityWeatherDetails);
        }
        catch (Exception ex)
        {
            return this.StatusCode((int)HttpStatusCode.InternalServerError,
                "An error occurred while getting the pinned city weather details: " + ex.Message);
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
        if (city == null) return this.NotFound();

        // Perform authorization
        var authorizationResult =
            await authorizationService.AuthorizeAsync(this.HttpContext.User, city, Operations.Read);

        if (!authorizationResult.Succeeded)
        {
            if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

            return new ChallengeResult();
        }

        // Get the temperature history
        try
        {
            if (cityId == Guid.Empty) return this.BadRequest("Invalid id parameter");

            var pinnedCityDailyTemperatures = await pinnedCityWeatherService.GetWeatherHistoryAsync(cityId);

            if (pinnedCityDailyTemperatures == null) return this.NotFound();

            return this.Ok(pinnedCityDailyTemperatures);
        }
        catch (Exception ex)
        {
            return this.StatusCode((int)HttpStatusCode.InternalServerError,
                "An error occurred while getting the pinned city weather history: " + ex.Message);
        }
    }
}