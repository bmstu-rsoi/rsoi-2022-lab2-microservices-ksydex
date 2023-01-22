using LoyaltyService.Data.Entities;

namespace LoyaltyService.Helpers;

public static class LoyaltyHelpers
{
    public static class Types
    {
        public const string Bronze = "BRONZE";
        public const string Silver = "SILVER";
        public const string Gold = "GOLD";
    }

    public static string CalcStatus(Loyalty loyalty)
        => loyalty.ReservationCount switch
        {
            >= 20 => Types.Gold,
            >= 10 => Types.Silver,
            _ => Types.Bronze
        };
    
    public static int CalcDiscount(Loyalty loyalty)
        => loyalty.Status switch
        {
            Types.Bronze => 5,
            Types.Silver => 7,
            Types.Gold => 10,
            _ => throw new Exception("Wrong loyalty status")
        };

}