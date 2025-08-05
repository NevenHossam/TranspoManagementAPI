using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspoManagementAPI.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.1, 10.0)]
        public double FareMultiplier { get; set; } = 1.0;

        public ICollection<Trip> Trips { get; set; }
    }
}
