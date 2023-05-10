﻿using FullStack.API.Models;
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
        public async Task<IActionResult> GetAllBreastfeedings()
        {
            var breastfeedings = await _breastfeedingService.GetAllBreastfeedings();
            return Ok(breastfeedings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreastfeedingById(int id)
        {
            var breastfeeding = await _breastfeedingService.GetBreastfeedingById(id);
            if (breastfeeding == null)
            {
                return NotFound();
            }
            return Ok(breastfeeding);
        }



        [HttpGet("Today")]
        public async Task<IActionResult> GetBreastfeedingByToday([FromQuery] string clientTimeZone)
        {
            var breastFeedings = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);

            return Ok(breastFeedings);
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
            if(breastfeeding.Id == id)
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

