using Microsoft.Extensions.Configuration;

namespace Tests.Help;

public static class JwtTestConfiguration
{
    public static IConfiguration Create()
    {
        var dict = new Dictionary<string, string>
        {
            ["Jwt:Key"] = "super_secret_test_key_123456789",
            ["Jwt:Issuer"] = "test_issuer",
            ["Jwt:Audience"] = "test_audience",
            ["Jwt:ExpiresMinutes"] = "60"
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(dict!)
            .Build();
    }
}