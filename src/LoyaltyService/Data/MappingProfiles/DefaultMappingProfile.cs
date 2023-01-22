using AutoMapper;
using LoyaltyService.Data.Dtos;
using LoyaltyService.Data.Entities;

namespace LoyaltyService.Data.MappingProfiles;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<Loyalty, LoyaltyDto>();
    }
}