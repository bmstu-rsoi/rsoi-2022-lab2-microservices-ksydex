namespace Gateway.Data.Dtos;

public class ReservationDto
{
    public Guid ReservationUid { get; set; }
    
    public string UserName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Guid PaymentUid { get; set; }
    
    public PaymentDto? Payment { get; set; }

    public HotelDto? Hotel { get; set; }
    public Guid? HotelUid { get; set; }
}