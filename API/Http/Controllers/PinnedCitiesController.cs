using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class PinnedCitiesController(IPinnedCityService pinnedCityService) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<PinnedCityDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync()
    {
        var cities = await pinnedCityService.GetForUserAsync(HttpContext.User);
        return Ok(cities);
    }

    [HttpGet("Paginated")]
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
        var city = await pinnedCityService.GetByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return CreatedAtAction(nameof(CreateAsync), city);
    }

    [HttpPut]
    [Authorize]
    [ActionName(nameof(UpdateAsync))]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid id parameter");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await pinnedCityService.UpdatePinnedCity(id, pinnedCityDto);

            return Ok("Pinned city updated successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the pinned city: " + ex.Message);
        }
    }

}