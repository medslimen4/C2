using Microsoft.AspNetCore.Mvc;
using C2.Domain.IDAO;
using LaverieEntities.Entities;
using C2.Domain.DTO.CreateDTO;
using LaverieEntities.IDAO;

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

        // PUT: /Machine/{idMachine}
        [HttpPut("{idMachine}")]
        public IActionResult Update(int idMachine, [FromBody] CreateMachineDTO machine)
        {
            if (machine == null)
            {
                return BadRequest("Machine cannot be null.");
            }

            try
            {
                var existingMachine = _daoMachine.GetMachineById(idMachine);

                if (existingMachine == null)
                {
                    return NotFound();
                }

                // Create a CreateMachineDTO from the existing machine
                CreateMachineDTO updatedMachine = new CreateMachineDTO
                {
                    IdMachine = existingMachine.IdMachine,
                    MarqueMachine = machine.MarqueMachine,
                    EtatMachine = machine.EtatMachine,
                    IDLaverie = machine.IDLaverie
                };

                // Call the update method with the CreateMachineDTO
                _daoMachine.UpdateMachine(updatedMachine);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: /machines/{id}/etat
        [HttpPut("{id}/etat")]
        public IActionResult UpdateMachineEtat(int id, [FromBody] string newEtat)
        {
            if (string.IsNullOrEmpty(newEtat))
            {
                return BadRequest("Le nouvel état ne peut pas être null ou vide.");
            }

            try
            {
                var existingMachine = _daoMachine.GetMachineById(id);
                if (existingMachine == null)
                {
                    return NotFound($"Machine avec ID {id} introuvable.");
                }


                var machineDto = new CreateMachineDTO
                {
                    IdMachine = existingMachine.IdMachine,
                    MarqueMachine = existingMachine.MarqueMachine,
                    EtatMachine = newEtat,
                    IDLaverie = existingMachine.IDLaverie
                };

                _daoMachine.UpdateMachine(machineDto);
                return Ok($"L'état de la machine ID {id} a été mis à jour à {newEtat}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
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
