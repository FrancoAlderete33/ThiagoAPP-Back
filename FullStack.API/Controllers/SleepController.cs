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
            var sleeps = await _sleepServices.GetAllSleeps();
            return Ok(sleeps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSleepById(int id)
        {
            var sleep = await _sleepServices.GetSleepById(id);
            if (sleep == null)
            {
                return NotFound();
            }

            return Ok(sleep);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetAllByToday(string clientTimeZone)
        {
            var sleeps = await _sleepServices.GetSleepsByToday(clientTimeZone);
            return Ok(sleeps);
        }

        [HttpGet("ByDate")]
        public async Task<IActionResult> GetSleepsByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                var result = await _sleepServices.GetSleepsByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateSleep(Sleep sleep)
        {
            if (sleep == null || sleep.start_time == null || sleep.end_time == null)
            {
                return BadRequest("Sleep data is invalid");
            }

            //if (sleep.start_time >= sleep.end_time)
            //{
            //    return BadRequest("Sleep start time must be before end time");
            //}

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
