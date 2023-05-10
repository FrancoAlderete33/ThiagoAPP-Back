using FullStack.API.Models;
using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        readonly ICalendarServices _calendarServices;

        public CalendarController(ICalendarServices calendarServices)
        {
            _calendarServices = calendarServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _calendarServices.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventById = await _calendarServices.GetEventById(id);
            if (eventById == null)
            {
                return NotFound();
            }
            return Ok(eventById);
        }



        [HttpGet("Today")]
        public async Task<IActionResult> GetEventsByToday([FromQuery] string clientTimeZone)
        {
            var events = await _calendarServices.GetEventsByToday(clientTimeZone);

            return Ok(events);
        }

        [HttpGet("ByDate")]
        public async Task<IActionResult> GetBreastfeedingByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                var result = await _breastfeedingService.GetBreastfeedingByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("NewOne")]

        public async Task<IActionResult> CreateBreastfeeding([FromBody] Breastfeeding breastfeeding)
        {
            decimal duration = (decimal)(breastfeeding.end_time - breastfeeding.start_time).TotalMinutes;
            breastfeeding.durationInMinutes = duration;

            try
            {
                await _breastfeedingService.CreateBreastfeeding(breastfeeding);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el registro de lactancia.", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBreastfeeding(int id, [FromBody] Breastfeeding breastfeeding)
        {
            if (breastfeeding.Id == id)
            {
                await _breastfeedingService.UpdateBreastfeeding(breastfeeding);
            }
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBreastfeeding(int id)
        {
            await _breastfeedingService.DeleteBreastfeeding(id);
            return Ok();
        }
    }
}
}
