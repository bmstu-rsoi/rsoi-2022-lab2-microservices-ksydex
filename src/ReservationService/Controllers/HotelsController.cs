using System.Linq.Expressions;
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
    
    protected override Expression<Func<Hotel, bool>> UidPredicate(Guid uId)
        => x => x.HotelUid == uId;

    protected override void SetUid(Hotel entity, Guid uId)
        => entity.HotelUid = uId;

    protected override Guid? GetUid(Hotel e) => e.HotelUid;

    protected override void MapDtoToEntity(Hotel e, HotelDto dto)
    {
        e.Name = dto.Name;
        e.Address = dto.Address;
        e.City = dto.City;
        e.Country = dto.Country;
        e.Stars = dto.Stars;
        e.Price = dto.Price;
    }
}