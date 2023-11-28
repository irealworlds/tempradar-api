using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Http.Controllers
{
    [ApiController]
    public class SensorsController(ISensorsService sensorsService) : ControllerBase
    {
        [HttpGet("Sensors")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SensorDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetSensors([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            try
            {
                var sensors = await sensorsService.GetSensorsAsync(skip, limit);

                if (sensors == null)
                {
                    return NotFound();
                }

                return Ok(sensors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the sensors: " + ex.Message);
            }
        }

        [HttpGet("SensorReadings")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SensorReadingDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetSensorReadings([FromQuery, Required] string resourceIdentifier, [FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            try
            {
                if (resourceIdentifier == null)
                {
                    return BadRequest("Invalid resourceIdentifier parameter");
                }

                var sensorReadings = await sensorsService.GetSensorReadingsAsync(resourceIdentifier, skip, limit);

                if (sensorReadings == null)
                {
                    return NotFound();
                }

                return Ok(sensorReadings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while getting the sensor readings data: " + ex.Message);
            }
        }
    }
}
