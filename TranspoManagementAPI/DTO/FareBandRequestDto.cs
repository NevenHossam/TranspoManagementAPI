namespace TranspoManagementAPI.DTO
{
    public class FareBandRequestDto
    {
        [System.ComponentModel.DataAnnotations.Range(0.0, double.MaxValue, ErrorMessage = "Distance limit must be non-negative")]
        public double? DistanceLimit { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Rate per mile is required")]
        [System.ComponentModel.DataAnnotations.Range(0.01, double.MaxValue, ErrorMessage = "Rate per mile must be positive")]
        public double RatePerMile { get; set; }
    }
}
