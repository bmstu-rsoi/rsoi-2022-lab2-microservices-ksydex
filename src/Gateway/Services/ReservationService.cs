using Gateway.Common;
using Gateway.Data.Dtos;
using Gateway.Helpers;
using SharedKernel.Exceptions;

namespace Gateway.Services;

public class ReservationClientService : ClientServiceBase
{
    private readonly LoyaltyClientService _loyaltyClientService;

    protected override string BaseUri => "http://reservation_service:80";

    public ReservationClientService(LoyaltyClientService loyaltyClientService)
    {
        _loyaltyClientService = loyaltyClientService;
    }

    public async Task<List<HotelDto>?> GetAllHotelsAsync(int page, int size)
        => await Client.GetAsync<List<HotelDto>>(BuildUri("api/v1/hotels"), null, new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "size", size.ToString() }
        });

    public async Task<HotelDto?> GetHotelByUIdAsync(string uid)
        => await Client.GetAsync<HotelDto>(BuildUri("api/v1/hotels/" + uid));

    public async Task BookHotelAsync(string hotelUid, DateTime startDate, DateTime endDate, string userName)
    {
        var hotel = await GetHotelByUIdAsync(hotelUid);
        if (hotel == null) throw new NotFoundException("Hotel not found");

        var nightCount = (endDate - startDate).Days;

        var loyalty = (await _loyaltyClientService.GetAllAsync(1, 1, userName))?.FirstOrDefault() ??
                      throw new NotFoundException("Loyalty not found");
        var cost = (hotel.Price * nightCount);
        cost = cost - cost * (loyalty.Discount / 100);


        // TODO: create payment
        // TODO: increment book count in loyalty service


        // return await Client.GetAsync<List<HotelDto>>(BuildUri("api/v1/hotels"), null, new Dictionary<string, string>
        // {
        //     { "page", page.ToString() },
        //     { "size", size.ToString() }
        // });
    }

    public async Task<List<ReservationDto>?> GetAllReservationsAsync(int page, int size, string userName)
        => await Client.GetAsync<List<ReservationDto>>(BuildUri("api/v1/reservations"), null,
            new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "size", size.ToString() },
                { "UserName", userName }
            });

    public async Task<ReservationDto?> GetReservationByUidAsync(string uid)
        => await Client.GetAsync<ReservationDto>(BuildUri("api/v1/reservations/" + uid));
}