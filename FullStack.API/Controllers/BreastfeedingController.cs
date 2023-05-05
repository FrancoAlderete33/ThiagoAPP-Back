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
        public async Task<IActionResult> GetAll()
        {
            var breastfeedings = await _breastfeedingService.GetAllBreastfeedings();
            return Ok(breastfeedings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var breastfeeding = await _breastfeedingService.GetBreastfeedingById(id);
            if (breastfeeding == null)
            {
                return NotFound();
            }
            return Ok(breastfeeding);
        }



        [HttpGet("Today")]
        public async Task<IActionResult> GetAllByToday(string clientTimeZone)
        {
            var breastFeedings = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);

            return Ok(breastFeedings);
        }
    

        [HttpPost("NewOne")]

        public async Task<IActionResult> Add(Breastfeeding breastfeeding)
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
        public async Task<IActionResult> Update(int id, Breastfeeding breastfeeding)
        {
            if(breastfeeding.Id == id)
            {
                await _breastfeedingService.UpdateBreastfeeding(breastfeeding);              
            }
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _breastfeedingService.DeleteBreastfeeding(id);
            return Ok();
        }
    }
}

