using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDatabase
{
    public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeSeedRoles()
        {
            //Se o perfil Admin não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                //Cria o perfil Admin
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            //Se o perfil Client não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                //Cria o perfil Client
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializeSeedUsers()
        {
            //Se o usuário Admin não existir cria o usuário, define a senha e atribui o perfil
            if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
            {
                //Define os dados do usuário Admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (14) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                //Cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "SenhaDura#2023").Result;
                if (resultAdmin.Succeeded)
                {
                    //Inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //Inclui as claims do usuário Admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            //Se o usuário Client não existir cria o usuário, define a senha e atribui o perfil
            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                //Define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (14) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "client1",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                //Cria o usuário client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "SenhaDura#2023").Result;
                if (resultClient.Succeeded)
                {
                    //Inclui o usuário client ao perfil client
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //Inclui as claims do usuário Admin
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, client.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, client.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }

        }
    }
}
