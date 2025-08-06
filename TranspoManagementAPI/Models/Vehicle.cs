using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspoManagementAPI.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double FareMultiplier { get; set; } = 1.0;

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
