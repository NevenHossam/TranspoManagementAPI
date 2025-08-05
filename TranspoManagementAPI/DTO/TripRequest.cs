namespace TranspoManagementAPI.DTO
{
    public class TripRequest
    {
        public double Distance { get; set; }
        public int VehicleId { get; set; }
        public DateTime TripDate { get; set; } = DateTime.UtcNow;
    }
}