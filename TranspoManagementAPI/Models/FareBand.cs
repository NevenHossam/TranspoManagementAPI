using System.ComponentModel.DataAnnotations;

namespace TranspoManagementAPI.Models
{
    public class FareBand
    {
        [Key]
        public int Id { get; set; }

        public double? DistanceLimit { get; set; }  // e.g., 1, 6, 16, or null for no limit

        [Required]
        [Range(0.01, double.MaxValue)]
        public double RatePerMile { get; set; }     // GBP per mile
    }
}

