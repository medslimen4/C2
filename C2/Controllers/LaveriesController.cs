using C2.Domain.DTO.CreateDTO;
using C2.Domain.IDAO;
using LaverieEntities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C2.Controllers
{


        [ApiController]
        [Route("[controller]")]
        public class LaveriesController : ControllerBase
        {
            private readonly ILaveriesDAO _daoLaveries;

            public LaveriesController(ILaveriesDAO daoLaveries)
            {
                _daoLaveries = daoLaveries;
            }

            // GET: /laveries
            [HttpGet]
            public IActionResult GetAll()
            {
                try
                {
                    var laveries = _daoLaveries.GetAllLaveries();
                    return Ok(laveries);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // GET: /laveries/{id}
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                try
                {
                    var laverie = _daoLaveries.GetLaverieById(id);
                    if (laverie == null)
                    {
                        return NotFound();
                    }
                    return Ok(laverie);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // POST: /laveries
            [HttpPost]
            public IActionResult Create([FromBody] CreateLaverieDTO laverie)
            {
                if (laverie == null)
                {
                    return BadRequest("Laverie cannot be null.");
                }
                try
                {
                    _daoLaveries.CreateLaverie(laverie);
                    return CreatedAtAction(nameof(GetById), new { id = laverie.IdLaverie }, laverie);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // PUT: /laveries
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateLaverieDTO laverie)
        {
            if (laverie == null)
            {
                return BadRequest("Laverie cannot be null.");
            }
            try
            {
                var existingLaverie = _daoLaveries.GetLaverieById(id); // Use the ID from the URL
                if (existingLaverie == null)
                {
                    return NotFound();
                }
                _daoLaveries.UpdateLaverie(laverie);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /laveries/{id}
        [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    var existingLaverie = _daoLaveries.GetLaverieById(id);
                    if (existingLaverie == null)
                    {
                        return NotFound();
                    }
                    _daoLaveries.DeleteLaverie(id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    

}
