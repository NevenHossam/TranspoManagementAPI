namespace TranspoManagementAPI.DTO
{
    public class VehicleRequest
    {
        public required string Name { get; set; }
        public double FareMultiplier { get; set; } = 1.0;
    }
}
