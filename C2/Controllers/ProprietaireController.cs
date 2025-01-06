using Microsoft.AspNetCore.Mvc;
using C2.Domain.IDAO;
using LaverieEntities.Entities;
using C2.Domain.DTO.CreateDTO;

namespace C2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProprietaireController : ControllerBase
    {
        private readonly IProprietaireDAO _proprietaireDAO;

        public ProprietaireController(IProprietaireDAO proprietaireDAO)
        {
            _proprietaireDAO = proprietaireDAO;
        }

        // GET: /proprietaire
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var proprietaires = _proprietaireDAO.GetAllProprietaires();
                return Ok(proprietaires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: /proprietaire/{cin}
        [HttpGet("{cin}")]
        public IActionResult GetByCin(int cin)
        {
            try
            {
                var proprietaire = _proprietaireDAO.GetProprietaireById(cin);
                if (proprietaire == null)
                {
                    return NotFound();
                }
                return Ok(proprietaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: /proprietaire
        [HttpPost]
        public IActionResult Create([FromBody] CreateProprietaireDTO proprietaire)
        {
            if (proprietaire == null)
            {
                return BadRequest("Proprietaire cannot be null.");
            }
            try
            {
                _proprietaireDAO.CreateProprietaire(proprietaire);
                return CreatedAtAction(nameof(GetByCin), new { cin = proprietaire._CIN }, proprietaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: /proprietaire
        [HttpPut]
        public IActionResult Update([FromBody] Proprietaire proprietaire)
        {
            if (proprietaire == null)
            {
                return BadRequest("Proprietaire cannot be null.");
            }
            try
            {
                var existingProprietaire = _proprietaireDAO.GetProprietaireById(proprietaire._CIN);
                if (existingProprietaire == null)
                {
                    return NotFound();
                }
                _proprietaireDAO.UpdateProprietaire(proprietaire);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /proprietaire/{cin}
        [HttpDelete("{cin}")]
        public IActionResult Delete(int cin)
        {
            try
            {
                var existingProprietaire = _proprietaireDAO.GetProprietaireById(cin);
                if (existingProprietaire == null)
                {
                    return NotFound();
                }
                _proprietaireDAO.DeleteProprietaire(cin);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
