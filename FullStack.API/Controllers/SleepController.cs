using FullStack.API.Models;
using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SleepController : ControllerBase
    {
        private readonly ISleepServices _sleepServices;

        public SleepController(ISleepServices sleepServices)
        {
            _sleepServices = sleepServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSleeps()
        {
            List<Sleep> sleeps = await _sleepServices.GetAllSleeps();
            return Ok(sleeps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSleepById(int id)
        {
            Sleep sleep = await _sleepServices.GetSleepById(id);
            if (sleep == null)
            {
                return NotFound();
            }

            return Ok(sleep);
        }

        /// <summary>
        /// Devuelve todos los periodos de sueño del dia actual.
        /// </summary>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los periodos de sueño</returns>
        [HttpGet("today")]
        public async Task<IActionResult> GetAllSleepsByToday(string clientTimeZone)
        {
            List<Sleep> sleeps = await _sleepServices.GetSleepsByToday(clientTimeZone);
            return Ok(sleeps);
        }

        /// <summary>
        /// Metodo para filtrar los periodos de sueño de una fecha especifica
        /// </summary>
        /// <param name="date">Fecha especifica que ha seleccionado el cliente</param>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los periodos de sueño de una fecha especifica</returns>
        [HttpGet("ByDate")]
        public async Task<IActionResult> GetSleepsByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                List<Sleep> result = await _sleepServices.GetSleepsByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para calcular el timpo neto de sueño 
        /// </summary>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con el calculo de la duracion total en minutos de todos los periodos de sueño cargados en el dia actual</returns>
        [HttpGet("today/duration")]
        public async Task<IActionResult> GetTotalSleepsDurationByToday(string clientTimeZone)
        {
            try
            {
                List<Sleep> sleeps = await _sleepServices.GetSleepsByToday(clientTimeZone);
                int durationInMinutes = 0;

                foreach (Sleep sleep in sleeps)
                {
                    durationInMinutes += Convert.ToInt32(sleep.durationInMinutes);
                }

                return Ok(durationInMinutes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateSleep(Sleep sleep)
        {
            if (sleep == null || sleep.start_time == null || sleep.end_time == null)
            {
                return BadRequest("Sleep data is invalid");
            }

            decimal duration = (decimal)(sleep.end_time - sleep.start_time).TotalMinutes;
            sleep.durationInMinutes = duration;

            try
            {
                await _sleepServices.CreateSleep(sleep);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating sleep record.", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSleep(int id, Sleep sleep)
        {
            if (sleep == null || sleep.Id != id)
            {
                return BadRequest("Invalid sleep data or Id");
            }

            await _sleepServices.UpdateSleep(sleep);
            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSleep(int id)
        {
            await _sleepServices.DeleteSleep(id);
            return Ok();
        }
    }
}
