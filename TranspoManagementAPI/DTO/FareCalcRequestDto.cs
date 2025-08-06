using System.ComponentModel.DataAnnotations;

namespace TranspoManagementAPI.DTO
{
    public class FareCalcRequestDto
    {
        [Required(ErrorMessage = "Distance is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Distance must be a non-negative value.")]
        public double Distance { get; set; }
    }
}
