using PaymentService.Constants;
using SharedKernel.Common.AbstractClasses;

namespace PaymentService.Data.Entities;

public class Payment : EntityBase
{
    public Guid PaymentUid { get; set; }
    public string Status { get; set; } = PaymentStatusTypes.Paid;
    public int Price { get; set; }
}