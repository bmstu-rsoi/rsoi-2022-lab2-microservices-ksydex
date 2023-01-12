namespace ReservationService.Models;

public class Reservation
{
    public int Id { get; set; }
    public Guid ReservationUid { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    public Guid PaymentUid { get; set; }

    public Hotel? Hotel { get; set; }
    public int? HotelId { get; set; }
}