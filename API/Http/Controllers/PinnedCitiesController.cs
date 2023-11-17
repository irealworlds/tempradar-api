using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class PinnedCitiesController(IPinnedCityService pinnedCityService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ActionName(nameof(CreateAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int) HttpStatusCode.Created)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePinnedCityDto pinnedCityDto)
    {
        var city = await pinnedCityService.CreateAsync(pinnedCityDto);
        return CreatedAtAction(nameof(ShowAsync), new { id = city.Id }, city);
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    [ActionName(nameof(ShowAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedCityDto), (int) HttpStatusCode.Created)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> ShowAsync(Guid id)
    {
        var city = await pinnedCityService.GetByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }
        
        return CreatedAtAction(nameof(CreateAsync), city);
    }
}