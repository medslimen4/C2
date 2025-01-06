using C2.Domain.DTO.CreateDTO;
using LaverieEntities.Entities;

namespace C2.Domain.IDAO
{
    public interface IProprietaireDAO
    {
        List<CreateProprietaireDTO> GetAllProprietaires();
        Proprietaire GetProprietaireById(int cin);
        void CreateProprietaire(CreateProprietaireDTO proprietaire);
        void UpdateProprietaire(Proprietaire proprietaire);
        void DeleteProprietaire(int cin);
    }
}
