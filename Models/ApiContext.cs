using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Crypto> Cryptos { get; set; }

        public DbSet<Wallet> Wallet { get; set; }
    }
}