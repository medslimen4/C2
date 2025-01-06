using C2.Domain.DTO.CreateDTO;
using LaverieEntities.Entities;

namespace C2.Domain.IDAO
{
    public interface IMachineDAO
    {
        List<Machine> GetAllMachines();
        Machine GetMachineById(int id);
        void CreateMachine(CreateMachineDTO machine);
        void UpdateMachine(CreateMachineDTO machine);
        void DeleteMachine(int id);
    }
}
