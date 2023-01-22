using LoyaltyService.Helpers;
using SharedKernel.Common.AbstractClasses;

namespace LoyaltyService.Data.Entities;

public class Loyalty : EntityBase
{
    public string UserName { get; set; } = string.Empty;
    public int ReservationCount { get; set; }
    public string Status { get; set; } = LoyaltyHelpers.Types.Bronze;
    public int Discount { get; set; }
}