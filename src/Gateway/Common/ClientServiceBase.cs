using Gateway.Helpers;

namespace Gateway.Common;

public abstract class ClientServiceBase
{
    private readonly string _baseUri;
    protected readonly HttpClientWrapper Client;


    public ClientServiceBase(string baseUri)
    {
        Client = new HttpClientWrapper();
        _baseUri = baseUri.Last() != '/' ? baseUri + "/" : baseUri;
    }

    protected string BuildUri(string append)
        => _baseUri + append;
}