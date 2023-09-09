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
            List<Calendar> events = await _calendarServices.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            Calendar eventById = await _calendarServices.GetEventById(id);
            if (eventById == null)
            {
                return NotFound();
            }
            return Ok(eventById);
        }



        /// <summary>
        /// Devuelve todos los eventos del dia actual.
        /// </summary>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los eventos</returns>
        [HttpGet("Today")]
        public async Task<IActionResult> GetEventsByToday([FromQuery] string clientTimeZone)
        {
            List<Calendar> events = await _calendarServices.GetEventsByToday(clientTimeZone);

            return Ok(events);
        }

        /// <summary>
        /// Metodo para filtrar los eventos de una fecha especifica.
        /// </summary>
        /// <param name="date">Fecha especifica que ha seleccionado el cliente</param>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los eventos de una fecha especifica</returns>
        [HttpGet("ByDate")]
        public async Task<IActionResult> GetEventsByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                List<Calendar> result = await _calendarServices.GetEventsByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("NewOne")]

        public async Task<IActionResult> CreateEvent([FromBody] Calendar calendar)
        {

            try
            {
                await _calendarServices.CreateEvent(calendar);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el evento.", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Calendar calendar)
        {
            if (calendar.Id == id)
            {
                await _calendarServices.UpdateEvent(calendar);
            }
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _calendarServices.DeleteEvent(id);
            return Ok();
        }
    }
}

