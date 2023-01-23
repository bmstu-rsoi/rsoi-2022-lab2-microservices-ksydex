using Gateway.Common;
using Gateway.Data.Dtos;

namespace Gateway.Services;

public class PaymentClientService : ClientServiceBase
{
    protected override string BaseUri => "http://payment_service:80";

    public async Task<List<PaymentDto>?> GetAllAsync(int page, int size)
        => await Client.GetAsync<List<PaymentDto>>(BuildUri("api/v1/payments"), null, new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "size", size.ToString() }
        });


    
    public async Task<PaymentDto?> GetByUidAsync(string uid)
        => await Client.GetAsync<PaymentDto>(BuildUri("api/v1/payments/" + uid));
    
    public async Task<PaymentDto?> UpdateAsync(int id, PaymentDto dto)
        => await Client.PatchAsync<PaymentDto, PaymentDto>(BuildUri("api/v1/payments/" + id), dto);

    public async Task<PaymentDto?> CreateAsync(PaymentDto dto)
        => await Client.PostAsync<PaymentDto, PaymentDto>(BuildUri("api/v1/payments"), dto);
}