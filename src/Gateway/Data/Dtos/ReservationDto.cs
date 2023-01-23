namespace Gateway.Data.Dtos;

public class ReservationDto
{
    public Guid ReservationUid { get; set; }
    
    public string UserName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    public Guid PaymentUid { get; set; }

    public HotelDto? Hotel { get; set; }
    public int? HotelId { get; set; }
}