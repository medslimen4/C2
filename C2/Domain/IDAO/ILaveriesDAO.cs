using C2.Domain.DTO.CreateDTO;
using LaverieEntities.Entities;

namespace C2.Domain.IDAO
{
    public interface ILaveriesDAO
    {
        List<Laveries> GetAllLaveries();
        Laveries GetLaverieById(int id);
        void CreateLaverie(CreateLaverieDTO laverie);
        void UpdateLaverie(CreateLaverieDTO laverie);
        void DeleteLaverie(int id);
    }
}
