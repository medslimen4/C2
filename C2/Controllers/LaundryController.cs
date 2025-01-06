using Microsoft.AspNetCore.Mvc;
using C2.Domain.IDAO;
using LaverieEntities.Entities;

namespace C2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaundryController : ControllerBase
    {
        private readonly IProprietaireDAO _proprietaireDAO;
        private readonly ILaveriesDAO _laveriesDAO;
        private readonly IMachineDAO _machineDAO;
        private readonly ICycleDAO _cycleDAO;

        public LaundryController(
            IProprietaireDAO proprietaireDAO,
            ILaveriesDAO laveriesDAO,
            IMachineDAO machineDAO,
            ICycleDAO cycleDAO)
        {
            _proprietaireDAO = proprietaireDAO;
            _laveriesDAO = laveriesDAO;
            _machineDAO = machineDAO;
            _cycleDAO = cycleDAO;
        }

        [HttpGet]
        public IActionResult GetAllData()
        {
            var proprietaires = _proprietaireDAO.GetAllProprietaires();
            var laveries = _laveriesDAO.GetAllLaveries();
            var machines = _machineDAO.GetAllMachines();
            var cycles = _cycleDAO.GetAllCycles();

            var result = new
            {
                Proprietaires = proprietaires.Select(p => new
                {
                    _CIN = p._CIN,
                    _Surname = p._Surname,
                    propLaverie = laveries.Where(l => l.ProprietaireCIN == p._CIN).Select(l => new
                    {
                        IdLaverie = l.IdLaverie,
                        CapaciteLaverie = l.CapaciteLaverie,
                        AddresseLaverie = l.AddresseLaverie,
                        machinesLaverie = machines.Where(m => m.IDLaverie == l.IdLaverie).Select(m => new
                        {
                            IdMachine = m.IdMachine,
                            MarqueMachine = m.MarqueMachine,
                            EtatMachine = m.EtatMachine,
                            cyclesMachine = cycles.Where(c => c.IdMachine == m.IdMachine).Select(c => new
                            {
                                IdCycle = c.IdCycle,
                                NomCycle = c.NomCycle,
                                DureeCycleHR = c.DureeCycleHR,
                                coutCycle = c.coutCycle
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Ok(result);
        }
    }
}
