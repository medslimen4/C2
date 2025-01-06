namespace C2.Domain.DTO.ReadDTO
{
    public class ReadMachineDTO
    {
        public int IdMachine { get; set; }
        public string MarqueMachine { get; set; }
        public string EtatMachine { get; set; }
        public int? IDLaverie { get; set; }
        public List<int> CyclesMachineIds { get; set; } = new List<int>();
    }
}
