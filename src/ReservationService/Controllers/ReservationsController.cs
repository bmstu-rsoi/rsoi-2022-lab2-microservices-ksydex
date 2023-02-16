using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationService.Common;
using ReservationService.Data;
using ReservationService.Data.Dtos;
using ReservationService.Data.Entities;
using ReservationService.Data.Filters;
using SharedKernel.Extensions;

namespace ReservationService.Controllers;

public class ReservationsController : ControllerCrudBase<Reservation, ReservationDto, ReservationFilter>
{
    public ReservationsController(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
    {
    }

    protected override Expression<Func<Reservation, bool>> UidPredicate(Guid uId)
    => x => x.ReservationUid == uId;

    protected override void SetUid(Reservation entity, Guid uId)
    => entity.ReservationUid = uId;

    protected override Guid? GetUid(Reservation e) => e.ReservationUid;

    protected override IQueryable<Reservation> AttachFilterToQueryable(IQueryable<Reservation> q, ReservationFilter f)
        => q.WhereNext(f.UserName, x => x.Username == f.UserName);

    protected override void MapDtoToEntity(Reservation e, ReservationDto dto)
    {
        e.Username = dto.UserName;
        e.Status = dto.Status;
        e.StartDate = dto.StartDate;
        e.EndDate = dto.EndDate;
        e.PaymentUid = dto.PaymentUid;
        e.HotelUid = dto.HotelUid;

    }

    protected override async Task<ReservationDto> ToDtoAsync(Reservation e)
    {
        var dto = Mapper.Map<ReservationDto>(e);

        dto.Hotel = Mapper.Map<HotelDto>(await DbContext.Set<Hotel>()
            .SingleAsync(x => x.HotelUid == e.HotelUid));

        return dto;
    }
}