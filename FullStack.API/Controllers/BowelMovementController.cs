using FullStack.API.Models;
using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BowelMovementController : ControllerBase
    {
        private readonly IBowelMovementService _bowelMovementService;

        public BowelMovementController(IBowelMovementService bowelMovementService)
        {
            _bowelMovementService = bowelMovementService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBowelMovements()
        {
            var bowelMovements = await _bowelMovementService.GetAllBowelMovements();
            return Ok(bowelMovements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBowelMovementById(int id)
        {
            try
            {
                var result = await _bowelMovementService.GetBowelMovementsById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Today")]
        public async Task<IActionResult> GetBowelMovementByToday(string clientTimeZone)
        {
            var bowelMovements = await _bowelMovementService.GetBowelMovementByToday(clientTimeZone);

            return Ok(bowelMovements);
        }

        [HttpGet("ByDate")]
        public async Task<IActionResult> GetBowelMovementByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                var result = await _bowelMovementService.GetBowelMovementByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NewOne")]
        public async Task<IActionResult> CreateBowelMovement(BowelMovement bowelMovement)
        {
            try
            {
                var result = await _bowelMovementService.CreateBowelMovement(bowelMovement);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBowelMovement(int id, BowelMovement bowelMovement)
        {
            try
            {
                await _bowelMovementService.UpdateBowelMovement(id, bowelMovement);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBowelMovement(int id)
        {
            try
            {
                await _bowelMovementService.DeleteBowelMovement(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
