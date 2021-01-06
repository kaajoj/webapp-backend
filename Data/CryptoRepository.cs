using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Data
{
    public class CryptoRepository : Repository<Crypto>, ICryptoRepository
    {
        public CryptoRepository(ApiContext context) : base(context) { }

        public ApiContext apiContext => _databaseContext as ApiContext;

        public Crypto GetCryptoByIdCrypto(int idCrypto)
        {
            return apiContext.Cryptos.FirstOrDefault(c => c.IdCrypto == idCrypto);
        }

        public async Task<Crypto> GetCryptoByRank(int? rank)
        {
            return await apiContext.Cryptos.FirstOrDefaultAsync(c => c.Rank == rank);
        }
    }
}
