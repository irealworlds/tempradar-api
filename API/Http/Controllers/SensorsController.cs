using System.ComponentModel.DataAnnotations;
using System.Net;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Dto.SensorApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorsController(ISensorsService sensorsService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedResultDto<SensorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetSensors([FromQuery] PaginationOptionsDto pagination)
    {
        try
        {
            var sensors = await sensorsService.GetSensorsAsync(pagination.Skip, pagination.Limit);

            return this.Ok(sensors);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, "An error occurred while getting the sensors: " + ex.Message);
        }
    }

    [Authorize]
    [HttpGet("{sensorId:required}/Readings")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedResultDto<SensorReadingDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetSensorReadings(string sensorId, [FromQuery] PaginationOptionsDto pagination)
    {
        try
        {
            var sensorReadings = await sensorsService.GetSensorReadingsAsync(sensorId, pagination.Skip, pagination.Limit);

            return this.Ok(sensorReadings);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, "An error occurred while getting the sensor readings data: " + ex.Message);
        }
    }
}