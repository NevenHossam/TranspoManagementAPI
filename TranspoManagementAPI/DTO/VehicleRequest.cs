namespace TranspoManagementAPI.DTO
{
    public class VehicleRequest
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vehicle name is required")]
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0.1, 10.0, ErrorMessage = "Fare multiplier must be between 0.1 and 10.0")]
        public double FareMultiplier { get; set; } = 1.0;
    }
}
