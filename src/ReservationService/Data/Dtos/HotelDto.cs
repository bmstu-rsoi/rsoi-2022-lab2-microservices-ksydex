namespace ReservationService.Data.Dtos;

public class HotelDto
{
    public int Id { get; set; }
    public Guid HotelUid { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? Stars { get; set; }
    public int Price { get; set; }

    public string FullAddress { get; set; } = string.Empty;
}