using C2.Domain.DTO.CreateDTO;
using LaverieEntities.Entities;

namespace C2.Domain.IDAO
{
    public interface ICycleDAO
    {
        List<Cycle> GetAllCycles();
        Cycle GetCycleById(int id);
        void CreateCycle(CreateCycleDTO cycle);
        void UpdateCycle(Cycle cycle);
        void DeleteCycle(int id);
    }

}
