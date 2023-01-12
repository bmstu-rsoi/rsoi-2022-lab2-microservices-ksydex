namespace SharedKernel.Helpers;

public class ConnectionStringHelpers
{
    public static string? GetProductionDbConnectionString(string? v)
    {
        var connectionUrl = v ?? Environment.GetEnvironmentVariable("DATABASE_URL");

        if (connectionUrl == null) return null;
    
        connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
        var userPassSide = connectionUrl.Split("@")[0];
        var hostSide = connectionUrl.Split("@")[1];

        var user = userPassSide.Split(":")[0];
        var password = userPassSide.Split(":")[1];
        var host = hostSide.Split("/")[0];
        var database = hostSide.Split("/")[1].Split("?")[0];

        return $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
}