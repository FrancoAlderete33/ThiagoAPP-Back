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
        public async Task<IActionResult> GetAllBreastfeedings()
        {
            List<Breastfeeding> breastfeedings = await _breastfeedingService.GetAllBreastfeedings();
            return Ok(breastfeedings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreastfeedingById(int id)
        {
            Breastfeeding breastfeeding = await _breastfeedingService.GetBreastfeedingById(id);
            if (breastfeeding == null)
            {
                return NotFound();
            }
            return Ok(breastfeeding);
        }


        /// <summary>
        /// Devuelve todos los periodos de lactancia del dia actual.
        /// </summary>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los periodos de lactancia</returns>
        [HttpGet("Today")]
        public async Task<IActionResult> GetBreastfeedingByToday([FromQuery] string clientTimeZone)
        {
            List<Breastfeeding> breastFeedings = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);

            return Ok(breastFeedings);
        }

        /// <summary>
        /// Metodo para filtrar los periodos de lactancia de una fecha especifica
        /// </summary>
        /// <param name="date">Fecha especifica que ha seleccionado el cliente</param>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con los periodos de lactancia de una fecha especifica</returns>
        [HttpGet("ByDate")]
        public async Task<IActionResult> GetBreastfeedingByDate([FromQuery] DateTime date, string clientTimeZone)
        {
            try
            {
                List<Breastfeeding> result = await _breastfeedingService.GetBreastfeedingByDate(date, clientTimeZone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Metodo para calcular el timpo neto de lactancia. 
        /// </summary>
        /// <param name="clientTimeZone">Identifica el uso horario del cliente</param>
        /// <returns>Devuelve un estado 200 con el calculo de la duracion total en minutos de todos los periodos de lactancia cargados en el dia actual</returns>
        [HttpGet("today/duration")]
        public async Task<IActionResult> GetTotalBreastfeedingDurationByToday(string clientTimeZone)
        {
            try
            {
                List<Breastfeeding> breastfeedings = await _breastfeedingService.GetBreastfeedingByToday(clientTimeZone);
                int durationInMinutes = 0;

                foreach (Breastfeeding breastfeeding in breastfeedings)
                {
                    durationInMinutes += Convert.ToInt32(breastfeeding.durationInMinutes);
                }

                return Ok(durationInMinutes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
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

