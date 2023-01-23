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

    protected override IQueryable<Reservation> AttachEagerLoadingStrategyToQueryable(IQueryable<Reservation> q)
        => q.Include(x => x.Hotel);

    protected override void MapDtoToEntity(Reservation e, ReservationDto dto)
    {
        e.Username = dto.UserName;
        e.Status = dto.Status;
        e.StartDate = dto.StartDate;
        e.EndDate = dto.EndDate;
        e.PaymentUid = dto.PaymentUid;
        e.HotelUid = dto.HotelUid;

    }
}