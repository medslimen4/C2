namespace C2.Domain.DTO.ReadDTO
{
    public class ReadProprietaireDTO
    {
        public int CIN { get; set; }
        public string Surname { get; set; }
        public List<int> PropLaverieIds { get; set; } = new List<int>();
    }
}
