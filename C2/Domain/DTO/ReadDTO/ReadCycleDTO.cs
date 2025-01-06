namespace C2.Domain.DTO.ReadDTO
{
    public class ReadCycleDTO
    {
        public int IdCycle { get; set; }
        public string NomCycle { get; set; }
        public int DureeCycleHR { get; set; }
        public double CoutCycle { get; set; }
        public int? IdMachine { get; set; }
    }
}
