using System.Linq.Expressions;
using AutoMapper;
using PaymentService.Common;
using PaymentService.Data;
using PaymentService.Data.Dtos;
using PaymentService.Data.Entities;
using PaymentService.Data.Filters;
using SharedKernel.Extensions;

namespace PaymentService.Controllers;

public class PaymentsController : ControllerCrudBase<Payment, PaymentDto, PaymentFilter>
{
    public PaymentsController(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
    {
    }

    protected override Expression<Func<Payment, bool>> UidPredicate(Guid uId)
        => x => x.PaymentUid == uId;

    protected override void SetUid(Payment entity, Guid uId)
        => entity.PaymentUid = uId;

    protected override Guid? GetUid(Payment e) => e.PaymentUid;

    protected override void MapDtoToEntity(Payment e, PaymentDto dto)
    {
        e.Status = dto.Status;
        e.Price = dto.Price;
    }
}