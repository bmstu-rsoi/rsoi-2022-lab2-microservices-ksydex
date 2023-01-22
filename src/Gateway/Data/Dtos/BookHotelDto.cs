namespace Gateway.Data.Dtos;

public record BookHotelDto
{
    public string HotelUid { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}