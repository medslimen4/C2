using Microsoft.AspNetCore.Mvc;
using C2.Domain.IDAO;
using LaverieEntities.Entities;
using C2.Domain.DTO.CreateDTO;

namespace C2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineDAO _daoMachine;

        public MachineController(IMachineDAO daoMachine)
        {
            _daoMachine = daoMachine;
        }

        // GET: /machines
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var machines = _daoMachine.GetAllMachines();
                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: /machines/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var machine = _daoMachine.GetMachineById(id);
                if (machine == null)
                {
                    return NotFound();
                }
                return Ok(machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: /machines
        [HttpPost]
        public IActionResult Create([FromBody] CreateMachineDTO machine)
        {
            if (machine == null)
            {
                return BadRequest("Machine cannot be null.");
            }
            try
            {
                _daoMachine.CreateMachine(machine);
                return CreatedAtAction(nameof(GetById), new { id = machine.IdMachine }, machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: /machines
        [HttpPut]
        public IActionResult Update([FromBody] CreateMachineDTO machine)
        {
            if (machine == null)
            {
                return BadRequest("Machine cannot be null.");
            }
            try
            {
                var existingMachine = _daoMachine.GetMachineById(machine.IdMachine);
                if (existingMachine == null)
                {
                    return NotFound();
                }
                _daoMachine.UpdateMachine(machine);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /machines/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingMachine = _daoMachine.GetMachineById(id);
                if (existingMachine == null)
                {
                    return NotFound();
                }
                _daoMachine.DeleteMachine(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
