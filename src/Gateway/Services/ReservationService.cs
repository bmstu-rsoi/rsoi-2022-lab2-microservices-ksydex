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
        => await _client.GetAsync<List<HotelDto>>(BuildUri("api/v1/hotels"), new Dictionary<string, string>
        {
            {"page", page.ToString()},
            {"size", size.ToString()}
        });
}