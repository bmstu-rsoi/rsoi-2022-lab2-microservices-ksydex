using Gateway.Data.Dtos;
using Gateway.Helpers;

namespace Gateway.Services;

public class ReservationClientService
{
    private readonly HttpClientWrapper _client;
    private readonly string _baseUri;

    public ReservationClientService(string baseUri)
    {
        _client = new HttpClientWrapper();

        if (baseUri.Last() != '/') baseUri = baseUri + "/";
        _baseUri = baseUri;
    }

    protected string BuildUri(string append)
        => _baseUri + append;

    public async Task<List<HotelDto>?> GetAllHotelsAsync(int page, int size)
        => await _client.GetAsync<List<HotelDto>>(BuildUri("api/v1/hotels"), null, new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "size", size.ToString() }
        });

    public async Task<List<ReservationDto>?> GetAllReservationsAsync(int page, int size, string userName)
        => await _client.GetAsync<List<ReservationDto>>(BuildUri("api/v1/reservations"), null,
            new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "size", size.ToString() },
                { "UserName", userName }
            });

    public async Task<ReservationDto?> GetReservationByUidAsync(string uid)
        => await _client.GetAsync<ReservationDto>(BuildUri("api/v1/reservations/" + uid));
}