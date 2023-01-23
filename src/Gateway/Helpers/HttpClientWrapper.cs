using Newtonsoft.Json;
using SharedKernel.Helpers;

namespace Gateway.Helpers;

public class HttpClientWrapper
{
    private readonly HttpClient _client;

    public HttpClientWrapper()
    {
        _client = new HttpClient();
    }


    public async Task<T?> GetAsync<T>(string url, IDictionary<string, string>? headers = null,
        IDictionary<string, string>? queryParams = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(url, queryParams));
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        var response = await _client.SendAsync(request);

        await HttpExceptionHelpers.HandleResponseStatusCode(response);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response content: " + content);
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T?> PostAsync<T, TU>(string url, TU data, IDictionary<string, string>? headers = null,
        IDictionary<string, string>? queryParams = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, BuildUri(url, queryParams));
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        request.Content =
            new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        
        await HttpExceptionHelpers.HandleResponseStatusCode(response);
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T?> PatchAsync<T, TU>(string url, TU data, IDictionary<string, string>? headers = null,
        IDictionary<string, string>? queryParams = null)
    {
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), BuildUri(url, queryParams));
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        request.Content =
            new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        
        await HttpExceptionHelpers.HandleResponseStatusCode(response);
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task DeleteAsync(string url, IDictionary<string, string>? headers = null,
        IDictionary<string, string>? queryParams = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, BuildUri(url, queryParams));
        
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        await HttpExceptionHelpers.HandleResponseStatusCode(await _client.SendAsync(request));
    }

    private static Uri BuildUri(string url, IDictionary<string, string>? queryParams)
    {
        if (queryParams != null)
        {
            var query = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
            url += $"?{query}";
        }

        return new Uri(url);
    }
}