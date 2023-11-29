using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Authorization.Requirements;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class PinnedCitiesController(IPinnedCityService pinnedCityService, IAuthorizationService authorizationService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<PinnedCityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync()
    {
        var cities = await pinnedCityService.GetForUserAsync(HttpContext.User);
        return Ok(cities);
    }

    [HttpGet("Paginated")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedResultDto<PinnedCityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync([FromQuery] PaginationOptionsDto pagination)
    {
        var cities = await pinnedCityService.GetPaginatedForUserAsync(HttpContext.User, pagination);
        return Ok(cities);
    }

    [HttpPost]
    [Authorize]
    [ActionName(nameof(CreateAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        var city = await pinnedCityService.CreateForUserAsync(HttpContext.User, pinnedCityDto);
        return CreatedAtAction(nameof(ShowAsync), new { id = city.Id }, city);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ActionName(nameof(ShowAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ShowAsync(Guid id)
    {
        // Fetch the city details
        var city = await pinnedCityService.GetByIdAsync(id);

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

        // Return the city data
        return Ok(city);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ActionName(nameof(UpdateAsync))]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        try
        {
            // Fetch the city details
            var city = await pinnedCityService.GetByIdAsync(id);

            // Make sure a city with that ID actually exists
            if (city == null)
            {
                return NotFound();
            }

            // Perform authorization
            var authorizationResult = await authorizationService.AuthorizeAsync(HttpContext.User, city, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                if (User.Identity is { IsAuthenticated: true })
                {
                    return new ForbidResult();
                }

                return new ChallengeResult();
            }
            
            var updatedCity = await pinnedCityService.UpdatePinnedCityAsync(city, pinnedCityDto);

            return Ok(updatedCity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the pinned city: " + ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ActionName(nameof(DeleteAsync))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Fetch the city details
        var city = await pinnedCityService.GetByIdAsync(id);

        // Make sure a city with that ID actually exists
        if (city == null)
        {
            return NotFound();
        }

        // Perform authorization
        var authorizationResult = await authorizationService.AuthorizeAsync(HttpContext.User, city, Operations.Delete);

        if (!authorizationResult.Succeeded)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return new ForbidResult();
            }

            return new ChallengeResult();
        }
        
        // Delete the city
        try
        {
            await pinnedCityService.DeletePinnedCityAsync(id);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            if (exception.ParamName is nameof(id))
            {
                return NotFound();
            }

            throw;
        }
        catch (Exception ex)
        {
            return StatusCode(
                (int) HttpStatusCode.InternalServerError,
                $"An error occurred while deleting the pinned city: {ex.Message}"
            );
        }
    }
}