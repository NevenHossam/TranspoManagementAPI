using System;

namespace TranspoManagementAPI.DTO
{
    public class TripResponseDto
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public double TripTotalFare { get; set; }
        public DateTime TripDate { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
    }
}
