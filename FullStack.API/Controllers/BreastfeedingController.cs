using FullStack.API.Models;
using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreastfeedingController : ControllerBase
    {
        readonly IBreastfeedingServices _breastfeedingService;

        public BreastfeedingController(IBreastfeedingServices breastfeedingService)
        {
            _breastfeedingService = breastfeedingService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllBreastfeedings()
        {
            List<Breastfeeding> breastfeedings = await _breastfeedingService.GetAllBreastfeedings();

            return Ok(breastfeedings);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBreastfeedingById(int id)
        {
            Breastfeeding breastfeeding = await _breastfeedingService.GetBreastfeedingById(id);

            if (breastfeeding == null)
            {
                return NotFound();
            }

            return Ok(breastfeeding);
        }

        [HttpGet("Today")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBreastfeedingByToday([FromQuery] string clientTimeZone)
        {
            List<Breastfeeding> breastFeedingsByToday = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);

            return Ok(breastFeedingsByToday);
        }

        [HttpGet("ByDate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBreastfeedingByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            List<Breastfeeding> result = await _breastfeedingService.GetBreastfeedingByDate(date, clientTimeZone);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("today/duration")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTotalBreastfeedingDurationByToday(string clientTimeZone)
        {
            List<Breastfeeding> breastfeedings = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);

            int durationInMinutes = 0;

            foreach (Breastfeeding breastfeeding in breastfeedings)
            {
                durationInMinutes += Convert.ToInt32(breastfeeding.durationInMinutes);
            }

            return Ok(durationInMinutes);

        }

        [HttpPost("NewOne")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateBreastfeeding([FromBody] Breastfeeding breastfeeding)
        {
            decimal duration = (decimal)(breastfeeding.end_time - breastfeeding.start_time).TotalMinutes;

            breastfeeding.durationInMinutes = duration;

            await _breastfeedingService.CreateBreastfeeding(breastfeeding);
            return Ok();
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

