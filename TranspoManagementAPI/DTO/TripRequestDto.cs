namespace TranspoManagementAPI.DTO
{
    public class TripRequestDto
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Distance is required")]
        [System.ComponentModel.DataAnnotations.Range(0.1, double.MaxValue, ErrorMessage = "Distance must be positive")]
        public double Distance { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "VehicleId is required")]
        public int VehicleId { get; set; }

        public DateTime TripDate { get; set; } = DateTime.UtcNow;
    }
}