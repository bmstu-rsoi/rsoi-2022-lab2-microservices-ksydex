using AutoMapper;
using ReservationService.Data.Dtos;
using ReservationService.Data.Entities;

namespace ReservationService.Data.MappingProfiles;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<Hotel, HotelDto>();
        CreateMap<Reservation, ReservationDto>();
    }
}