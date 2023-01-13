using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            //VShop é aplicação Web que vai acessar o IdentityServer para obter o token
            new ApiScope("VShop", "VShop Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data."),
        };

    public static IEnumerable<Client> clients =>
        new List<Client>
        {
            //Cliente genérico
            new Client
            {
                ClientId = "client",
                ClientSecrets = {new Secret("abcdefghij#1234567890".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials, //Precisa das credenciais do usuário
                AllowedScopes = {"read", "write", "profile"},
            },
            //Cliente VShop
            new Client
            {
                ClientId = "vshop",
                ClientSecrets = {new Secret("abcdefghij#1234567890".Sha256())},
                AllowedGrantTypes = GrantTypes.Code, //Via código
                RedirectUris = { "https://localhost:7188/signin-oidc" }, //Login
                PostLogoutRedirectUris = { "https://localhost:7188/signout-callback-oidc" }, //Logout
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                }
            }
        };
}
