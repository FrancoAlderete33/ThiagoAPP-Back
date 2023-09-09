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
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllBowelMovements()
        {
            List<BowelMovement> bowelMovements = await _bowelMovementService.GetAllBowelMovements();
            return Ok(bowelMovements);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBowelMovementById(int id)
        {
            BowelMovement result = await _bowelMovementService.GetBowelMovementsById(id);

            return Ok(result);
        }

        [HttpGet("Today")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBowelMovementByToday(string clientTimeZone)
        {
            List<BowelMovement> bowelMovements = await _bowelMovementService.GetBowelMovementByToday(clientTimeZone);

            return Ok(bowelMovements);
        }


        [HttpGet("ByDate")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetBowelMovementByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            List<BowelMovement> result = await _bowelMovementService.GetBowelMovementByDate(date, clientTimeZone);

            return Ok(result);
        }

        [HttpPost("NewOne")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateBowelMovement(BowelMovement bowelMovement)
        {
            BowelMovement result = await _bowelMovementService.CreateBowelMovement(bowelMovement);

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateBowelMovement(int id, BowelMovement bowelMovement)
        {
            await _bowelMovementService.UpdateBowelMovement(id, bowelMovement);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteBowelMovement(int id)
        {
            await _bowelMovementService.DeleteBowelMovement(id);

            return Ok();
        }

    }
}
