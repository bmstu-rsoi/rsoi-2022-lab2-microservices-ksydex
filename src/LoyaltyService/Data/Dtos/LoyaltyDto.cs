namespace LoyaltyService.Data.Dtos;

public class LoyaltyDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int ReservationCount { get; set; }
    public string Status { get; set; } = "";
    public int Discount { get; set; }
}