using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface ICryptoRepository : IRepository<Crypto>
    {
        Task AddRange(List<Crypto> cryptos);
        Crypto GetCryptoByIdCrypto(int idCrypto);
        Task<Crypto> GetCryptoByRank(int? rank);
    }
}
