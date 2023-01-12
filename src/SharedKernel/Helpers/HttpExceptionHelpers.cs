namespace SharedKernel.Helpers;

public static class HttpExceptionHelpers
{
    public static async Task HandleResponseStatusCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(), null, response.StatusCode);
    }
}