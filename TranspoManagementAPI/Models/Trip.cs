using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranspoManagementAPI.Models
{
    [Table("Trips")]
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        public double Distance { get; set; }

        public double TripTotalFare { get; set; }

        public DateTime TripDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
