using System.Net;
using API.Authorization.Requirements;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class PinnedSensorsController(IPinnedSensorService pinnedSensorService,
    IAuthorizationService authorizationService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<PinnedSensorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync()
    {
        var sensors = await pinnedSensorService.GetForUserAsync(this.HttpContext.User);
        return this.Ok(sensors);
    }

    [HttpGet("Paginated")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedResultDto<PinnedSensorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> IndexAsync([FromQuery] PaginationOptionsDto pagination)
    {
        var sensors = await pinnedSensorService.GetPaginatedForUserAsync(this.HttpContext.User, pagination);
        return this.Ok(sensors);
    }

    [HttpPost]
    [Authorize]
    [ActionName(nameof(PinnedSensorsController.CreateAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedSensorDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePinnedSensorDto pinnedSensorDto)
    {
        var sensor = await pinnedSensorService.CreateForUserAsync(this.HttpContext.User, pinnedSensorDto);
        return this.CreatedAtAction(nameof(PinnedSensorsController.ShowAsync), new { id = sensor.Id }, sensor);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedSensorsController.ShowAsync))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PinnedSensorDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ShowAsync(Guid id)
    {
        // Fetch the sensor details
        var sensor = await pinnedSensorService.GetByIdAsync(id);

        // Make sure a sensor with that ID actually exists
        if (sensor == null) return this.NotFound();

        // Perform authorization
        var authorizationResult =
            await authorizationService.AuthorizeAsync(this.HttpContext.User, sensor, Operations.Read);


        if (!authorizationResult.Succeeded)
        {
            if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

            return new ChallengeResult();
        }

        // Return the sensor data
        return this.Ok(sensor);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedSensorsController.UpdateAsync))]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreatePinnedSensorDto pinnedCityDto)
    {
        try
        {
            // Fetch the sensor details
            var sensor = await pinnedSensorService.GetByIdAsync(id);

            // Make sure a sensor with that ID actually exists
            if (sensor == null) return this.NotFound();

            // Perform authorization
            var authorizationResult =
                await authorizationService.AuthorizeAsync(this.HttpContext.User, sensor, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

                return new ChallengeResult();
            }

            var updatedSensor = await pinnedSensorService.UpdatePinnedSensorAsync(sensor, pinnedCityDto);

            return this.Ok(updatedSensor);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, "An error occurred while updating the pinned sensor: " + ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ActionName(nameof(PinnedSensorsController.DeleteAsync))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Fetch the sensor details
        var sensor = await pinnedSensorService.GetByIdAsync(id);

        // Make sure a sensor with that ID actually exists
        if (sensor == null) return this.NotFound();

        // Perform authorization
        var authorizationResult =
            await authorizationService.AuthorizeAsync(this.HttpContext.User, sensor, Operations.Delete);

        if (!authorizationResult.Succeeded)
        {
            if (this.User.Identity is { IsAuthenticated: true }) return new ForbidResult();

            return new ChallengeResult();
        }

        // Delete the sensor
        try
        {
            await pinnedSensorService.DeletePinnedSensorAsync(id);

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
                $"An error occurred while deleting the pinned sensor: {ex.Message}"
            );
        }
    }
}