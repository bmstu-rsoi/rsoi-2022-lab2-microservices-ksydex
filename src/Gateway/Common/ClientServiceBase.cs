using Gateway.Helpers;

namespace Gateway.Common;

public abstract class ClientServiceBase
{
    protected abstract string BaseUri { get; }
    protected readonly HttpClientWrapper Client;

    public ClientServiceBase()
    {
        Client = new HttpClientWrapper();
    }

    private string GetBaseUri()
        => BaseUri;
        
    protected string BuildUri(string append)
    {
        var baseUri = GetBaseUri();
        baseUri = baseUri.Last() != '/' ? baseUri + "/" : baseUri;
        
        return baseUri + append;
    }
}