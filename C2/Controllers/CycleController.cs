using Microsoft.AspNetCore.Mvc;
using LaverieEntities.Entities;
using LaverieEntities.IDAO;
using C2.Domain.IDAO;
using C2.Domain.DTO.CreateDTO;

namespace C2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CycleController : ControllerBase
    {
        private readonly ICycleDAO _daoCycle;

        public CycleController(ICycleDAO daoCycle)
        {
            _daoCycle = daoCycle;
        }

        // GET: /cycles
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var cycles = _daoCycle.GetAllCycles();
                return Ok(cycles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: /cycles/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var cycle = _daoCycle.GetCycleById(id);
                if (cycle == null)
                {
                    return NotFound();
                }
                return Ok(cycle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: /cycles
        [HttpPost]
        public IActionResult Create([FromBody] CreateCycleDTO cycle)
        {
            if (cycle == null)
            {
                return BadRequest("Cycle cannot be null.");
            }
            try
            {
                _daoCycle.CreateCycle(cycle);
                return CreatedAtAction(nameof(GetById), new { id = cycle.IdCycle }, cycle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: /cycles
        [HttpPut]
        public IActionResult Update([FromBody] Cycle cycle)
        {
            if (cycle == null)
            {
                return BadRequest("Cycle cannot be null.");
            }
            try
            {
                var existingCycle = _daoCycle.GetCycleById(cycle.IdCycle);
                if (existingCycle == null)
                {
                    return NotFound();
                }
                _daoCycle.UpdateCycle(cycle);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /cycles/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingCycle = _daoCycle.GetCycleById(id);
                if (existingCycle == null)
                {
                    return NotFound();
                }
                _daoCycle.DeleteCycle(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
