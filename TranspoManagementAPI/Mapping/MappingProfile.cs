using AutoMapper;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleRequestDto, Vehicle>();
            CreateMap<Vehicle, VehicleResponseDto>();
            CreateMap<TripRequestDto, Trip>();
            CreateMap<Trip, TripResponseDto>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle != null ? src.Vehicle.Name : null));
            CreateMap<FareBandRequestDto, FareBand>();
            CreateMap<FareBand, FareBandResponseDto>();
        }
    }
}
