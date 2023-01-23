namespace Gateway.Data.Dtos;

public class UserInfoDto
{
    public LoyaltyDto Loyalty { get; set; }
    public List<ReservationDto> Reservations { get; set; }

    public UserInfoDto(LoyaltyDto loyalty, List<ReservationDto> reservations)
    {
        Loyalty = loyalty;
        Reservations = reservations;
    }
}