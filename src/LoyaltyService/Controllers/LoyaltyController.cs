using System.Linq.Expressions;
using AutoMapper;
using LoyaltyService.Common;
using LoyaltyService.Data;
using LoyaltyService.Data.Dtos;
using LoyaltyService.Data.Entities;
using LoyaltyService.Data.Filters;
using LoyaltyService.Helpers;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Extensions;

namespace LoyaltyService.Controllers;

public class LoyaltyController : ControllerCrudBase<Loyalty, LoyaltyDto, LoyaltyFilter>
{
    public LoyaltyController(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
    {
    }

    protected override IQueryable<Loyalty> AttachFilterToQueryable(IQueryable<Loyalty> q, LoyaltyFilter f)
        => q.WhereNext(f.UserName, x => x.UserName == f.UserName);

    protected override void MapDtoToEntity(Loyalty e, LoyaltyDto dto)
    {
        e.UserName = dto.UserName;
        e.ReservationCount = dto.ReservationCount;

        e.Status = LoyaltyHelpers.CalcStatus(e);
        e.Discount = LoyaltyHelpers.CalcDiscount(e);
    }
}