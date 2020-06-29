using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer.Utilities 
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("ApiOne"),
            new ApiResource("ApiTwo"),
            new ApiResource("ApiGateway")
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId = "ApiTwo",
                ClientSecrets = {new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiOne"}
            },            
            new Client
            {
                ClientId = "ApiOne",
                ClientSecrets = {new Secret("client_secret".ToSha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiTwo"}
            },
        };
    }
}