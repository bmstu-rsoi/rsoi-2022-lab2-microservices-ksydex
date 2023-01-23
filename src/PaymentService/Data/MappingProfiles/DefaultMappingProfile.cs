using AutoMapper;
using PaymentService.Data.Dtos;
using PaymentService.Data.Entities;

namespace PaymentService.Data.MappingProfiles;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<Payment, PaymentDto>();
    }
}