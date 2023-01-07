namespace VShopIdentityServer.SeedDatabase;

public interface IDatabaseSeedInitializer
{
    void InitializeSeedRoles();
    void InitializeSeedUsers();
}
