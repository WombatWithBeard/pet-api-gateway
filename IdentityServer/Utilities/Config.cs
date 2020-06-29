using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer.Utilities
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("ServiceOneApi"),
            new ApiResource("ServiceTwoApi"),
            new ApiResource("ApiGateway")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "ServiceOne",
                ClientSecrets = {new Secret("OneSecret".ToSha256())},
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ServiceTwoApi", "ApiGateway"}
            },
            new Client
            {
                ClientId = "ServiceTwo",
                ClientSecrets = {new Secret("TwoSecret".ToSha256())},
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ServiceOneApi", "ApiGateway"}
            }
        };
    }
}