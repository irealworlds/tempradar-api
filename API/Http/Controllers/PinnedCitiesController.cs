using System.Net;
using API.Authorization.Requirements;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class PinnedCitiesController
    (IPinnedCityService pinnedCityService, IAuthorizationService authorizationService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<PinnedCityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync()
    {
        var cities = await pinnedCityService.GetForUserAsync(this.HttpContext.User);
        return this.Ok(cities);
    }

    [HttpGet("Paginated")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedResultDto<PinnedCityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync([FromQuery] PaginationOptionsDto pagination)
    {
        var cities = await pinnedCityService.GetPaginatedForUserAsync(this.HttpContext.User, pagination);
        return this.Ok(cities);
    }

    [HttpPost]
    [Authorize]
    [ActionName(nameof(PinnedCitiesController.CreateAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        var city = await pinnedCityService.CreateForUserAsync(this.HttpContext.User, pinnedCityDto);
        return this.CreatedAtAction(nameof(PinnedCitiesController.ShowAsync), new { id = city.Id }, city);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedCitiesController.ShowAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ShowAsync(Guid id)
    {
        // Fetch the city details
        var city = await pinnedCityService.GetByIdAsync(id);

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

        // Return the city data
        return this.Ok(city);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedCitiesController.UpdateAsync))]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        try
        {
            // Fetch the city details
            var city = await pinnedCityService.GetByIdAsync(id);

            // Make sure a city with that ID actually exists
            if (city == null) return this.NotFound();

            // Perform authorization
            var authorizationResult =
                await authorizationService.AuthorizeAsync(this.HttpContext.User, city, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

                return new ChallengeResult();
            }

            var updatedCity = await pinnedCityService.UpdatePinnedCityAsync(city, pinnedCityDto);

            return this.Ok(updatedCity);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, "An error occurred while updating the pinned city: " + ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedCitiesController.DeleteAsync))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Fetch the city details
        var city = await pinnedCityService.GetByIdAsync(id);

        // Make sure a city with that ID actually exists
        if (city == null) return this.NotFound();

        // Perform authorization
        var authorizationResult =
            await authorizationService.AuthorizeAsync(this.HttpContext.User, city, Operations.Delete);

        if (!authorizationResult.Succeeded)
        {
            if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

            return new ChallengeResult();
        }

        // Delete the city
        try
        {
            await pinnedCityService.DeletePinnedCityAsync(id);

            return this.NoContent();
        }
        catch (ArgumentException exception)
        {
            if (exception.ParamName is nameof(id)) return this.NotFound();

            throw;
        }
        catch (Exception ex)
        {
            return this.StatusCode(
                (int)HttpStatusCode.InternalServerError,
                $"An error occurred while deleting the pinned city: {ex.Message}"
            );
        }
    }
}