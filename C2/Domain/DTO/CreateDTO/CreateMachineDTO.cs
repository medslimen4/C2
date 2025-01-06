namespace C2.Domain.DTO.CreateDTO
{
    public class CreateMachineDTO
    {
        public int IdMachine { get; set; }

        public string MarqueMachine { get; set; }
        public string EtatMachine { get; set; }
        public int? IDLaverie { get; set; }
    }
}
