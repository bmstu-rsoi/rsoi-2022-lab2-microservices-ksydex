using AutoMapper;
using ReservationService.Common;
using ReservationService.Data;
using ReservationService.Data.Dtos;
using ReservationService.Data.Entities;
using ReservationService.Data.Filters;

namespace ReservationService.Controllers;

public class HotelsController : ControllerCrudBase<Hotel, HotelDto, HotelFilter>
{
    public HotelsController(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
    {
    }
}