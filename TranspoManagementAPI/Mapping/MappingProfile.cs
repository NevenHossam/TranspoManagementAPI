using AutoMapper;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleResponseDto>();
            CreateMap<VehicleRequest, Vehicle>();
            CreateMap<Trip, TripResponseDto>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle != null ? src.Vehicle.Name : null));
            CreateMap<FareBand, FareBandResponseDto>();
            CreateMap<FareBandRequestDto, FareBand>();
        }
    }
}
