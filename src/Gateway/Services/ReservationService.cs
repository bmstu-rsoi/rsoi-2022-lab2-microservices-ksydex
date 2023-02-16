using Gateway.Common;
using Gateway.Data.Dtos;
using Gateway.Helpers;
using SharedKernel.Common.AbstractClasses;
using SharedKernel.Exceptions;

namespace Gateway.Services;

public class ReservationClientService : ClientServiceBase
{
    private readonly LoyaltyClientService _loyaltyClientService;
    private readonly PaymentClientService _paymentClientService;

    protected override string BaseUri => "http://reservation_service:80";

    public ReservationClientService(LoyaltyClientService loyaltyClientService,
        PaymentClientService paymentClientService)
    {
        _loyaltyClientService = loyaltyClientService;
        _paymentClientService = paymentClientService;
    }

    public async Task<PaginationModel<HotelDto>?> GetAllHotelsAsync(int page, int size)
        => await Client.GetAsync<PaginationModel<HotelDto>>(BuildUri("api/v1/hotels"), null, new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "size", size.ToString() }
        });

    public async Task<HotelDto?> GetHotelByUIdAsync(string uid)
        => await Client.GetAsync<HotelDto>(BuildUri("api/v1/hotels/" + uid));

    public async Task<ReservationDto?> CancelBookHotelAsync(string uId, string userName)
    {
        var reservation = await GetReservationByUidAsync(uId);
        if (reservation == null || reservation.Status == "CANCELED") throw new NotFoundException();


        if (reservation.UserName != userName) throw new Exception("Is not your reservation");

        var payment = await _paymentClientService.GetByUidAsync(reservation.PaymentUid.ToString()) ??
                      throw new Exception("Payment not found");
        payment.Status = "CANCELED";

        payment = await _paymentClientService.UpdateAsync(payment.Id, payment);


        var loyalty = await _loyaltyClientService.GetAsync(userName) ??
                      throw new NotFoundException("Loyalty not found");

        loyalty.ReservationCount = loyalty.ReservationCount - 1;

        loyalty = await _loyaltyClientService.UpdateAsync(loyalty.Id, loyalty) ??
                  throw new Exception("Error while updating loyalty");

        reservation.Status = "CANCELED";
        return await UpdateReservationAsync(reservation.ReservationUid.ToString(), reservation);
    }

    public async Task<ReservationBookDto> BookHotelAsync(string hotelUid, DateTime startDate, DateTime endDate,
        string userName)
    {
        var hotel = await GetHotelByUIdAsync(hotelUid);
        if (hotel == null) throw new NotFoundException("Hotel not found");

        var nightCount = (endDate - startDate).Days;

        var loyalty = await _loyaltyClientService.GetAsync(userName) ??
                      throw new NotFoundException("Loyalty not found");
        var cost = (hotel.Price * nightCount);
        Console.WriteLine("Cost: {0}", cost);
        cost = (int)(cost * (1 - loyalty.Discount / 100.0));
        Console.WriteLine("Cost after : {0}", cost);
        var payment = await _paymentClientService.CreateAsync(new PaymentDto
        {
            Price = cost,
            Status = "PAID"
        });

        if (payment == null) throw new Exception("Error while creating payment");

        loyalty = await _loyaltyClientService.UpdateAsync(loyalty.Id, new LoyaltyDto
        {
            UserName = loyalty.UserName,
            ReservationCount = loyalty.ReservationCount + 1
        }) ?? throw new Exception("Error while updating loyalty");


        var reservation = await CreateReservationAsync(new ReservationDto
        {
            UserName = userName,
            StartDate = startDate,
            EndDate = endDate,
            HotelUid = hotel.HotelUid,
            PaymentUid = payment.PaymentUid,
            Status = "PAID"
        }) ?? throw new Exception("Error while creating reservation");

        var r = await GetReservationByUidAsync(reservation.ReservationUid.ToString());

        if (r == null) throw new Exception("Error");
        return new ReservationBookDto
        {
            ReservationUid = r.ReservationUid,
            HotelUid = r.HotelUid!.Value,
            StartDate = r.StartDate!.Value,
            EndDate = r.EndDate!.Value,
            Discount = loyalty.Discount,
            Status = r.Status,
            Payment = new PaymentBookDto
            {
                Status = payment.Status,
                Price = payment.Price
            }
        };
    }

    public async Task<ReservationDto?> CreateReservationAsync(ReservationDto dto)
        => await Client.PostAsync<ReservationDto, ReservationDto>(BuildUri("api/v1/reservations"), dto);


    public async Task<ReservationDto?> UpdateReservationAsync(string id, ReservationDto dto)
        => await Client.PatchAsync<ReservationDto, ReservationDto>(BuildUri("api/v1/reservations/" + id), dto);

    public async Task<PaginationModel<ReservationDto>?> GetAllReservationsAsync(int page, int size, string userName)
    {
        var r = await Client.GetAsync<PaginationModel<ReservationDto>>(BuildUri("api/v1/reservations"), null,
            new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "size", size.ToString() },
                { "UserName", userName }
            });

        if (r == null) throw new NotFoundException();
        
        foreach (var item in r.Items)
        {
            item.Payment = await _paymentClientService.GetByUidAsync(item.PaymentUid.ToString());
        }

        return r;
    }

    public async Task<ReservationDto?> GetReservationByUidAsync(string uid)
    {
        var r = await Client.GetAsync<ReservationDto>(BuildUri("api/v1/reservations/" + uid));
        if (r == null) throw new NotFoundException();

        r.Payment = await _paymentClientService.GetByUidAsync(r.PaymentUid.ToString());
        
        return r;
    }

    public async Task<UserInfoDto> GetUserInfoAsync(string userName)
    {
        var loyalty = await _loyaltyClientService.GetAsync(userName);
        if (loyalty == null) throw new NotFoundException();

        return new UserInfoDto(loyalty,
            (await GetAllReservationsAsync(1, 100, userName))?.Items ?? new List<ReservationDto>());
    }
}