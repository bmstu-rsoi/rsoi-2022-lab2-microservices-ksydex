using AutoMapper;
using PaymentService.Common;
using PaymentService.Data;
using PaymentService.Data.Dtos;
using PaymentService.Data.Entities;
using PaymentService.Data.Filters;
using SharedKernel.Extensions;

namespace PaymentService.Controllers;

public class LoyaltyController : ControllerCrudBase<Payment, PaymentDto, PaymentFilter>
{
    public LoyaltyController(IMapper mapper, AppDbContext dbContext) : base(mapper, dbContext)
    {
    }

}