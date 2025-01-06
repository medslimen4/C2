namespace C2.Domain.DTO.CreateDTO
{
    public class CreateLaverieDTO
    {
        public int IdLaverie { get; set; }

        public string? CapaciteLaverie { get; set; }
        public string? AddresseLaverie { get; set; }
        public int? ProprietaireCIN { get; set; }
    }
}
