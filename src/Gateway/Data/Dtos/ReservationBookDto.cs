namespace Gateway.Data.Dtos;

public class ReservationBookDto
{
    public Guid ReservationUid { get; set; }
    public Guid HotelUid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Discount { get; set; }
    public string Status { get; set; } = "";
    public PaymentBookDto Payment { get; set; } = null!;
}

public class PaymentBookDto
{
    public string Status { get; set; } = "";
    public decimal Price { get; set; }
}
