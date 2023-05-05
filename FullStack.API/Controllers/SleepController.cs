using FullStack.API.Models;
using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SleepController : ControllerBase
    {
        readonly ISleepServices _sleepServices;

        public SleepController(ISleepServices sleepServices)
        {
            _sleepServices = sleepServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Sleeps = await _sleepServices.GetAllSleeps();
            return Ok(Sleeps);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sleep = await _sleepServices.GetSleepById(id);
            if (sleep == null)
            {
                return NotFound();
            }
            return Ok(sleep);
        }




        [HttpGet("Today")]
        public async Task<IActionResult> GetAllByToday(string clientTimeZone)
        {
            var sleeps = await _sleepServices.GetSleepsByToday(clientTimeZone);

            return Ok(sleeps);
        }

        [HttpPost("NewOne")]
        public async Task<IActionResult> Add (Sleep sleep)
        {
            decimal duration = (decimal)(sleep.end_time - sleep.start_time).TotalMinutes;
            sleep.durationInMinutes = duration;

            try
            {
                await _sleepServices.CreateSleep(sleep);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el registro de sueño.", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update (int id , Sleep sleep)
        {
            if(sleep.Id == id)
            {
                await _sleepServices.UpdateSleep(sleep);
            }
            return Ok();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sleepServices.DeleteSleep(id);
            return Ok();
        }

    }
}
