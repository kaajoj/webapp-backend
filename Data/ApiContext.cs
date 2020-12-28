using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VSApi.Models;

namespace VSApi.Data
{
    public class ApiContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApiContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        { 
        }

        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
    }
}