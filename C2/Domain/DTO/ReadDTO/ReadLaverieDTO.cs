namespace C2.Domain.DTO.ReadDTO
{
    public class ReadLaverieDTO
    {
        public int IdLaverie { get; set; }
        public string? CapaciteLaverie { get; set; }
        public string? AddresseLaverie { get; set; }
        public List<int> MachinesLaverieIds { get; set; } = new List<int>();
        public int? ProprietaireCIN { get; set; }
    }
}
