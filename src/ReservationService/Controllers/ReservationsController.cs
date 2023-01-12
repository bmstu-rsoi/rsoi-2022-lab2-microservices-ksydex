using AutoMapper;
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

    protected override IQueryable<Reservation> AttachFilterToQueryable(IQueryable<Reservation> q, ReservationFilter f)
        => q.WhereNext(f.UserName, x => x.Username == f.UserName);
}