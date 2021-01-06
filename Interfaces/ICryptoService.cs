using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface ICryptoService
    {
        IEnumerable<Crypto> GetAll();
        Crypto Get(int id);
        Task<Crypto> AddAsync(Crypto crypto);
        Task<Crypto> UpdateAsync(Crypto crypto);
        Task<Crypto> GetCryptoByRank(int? rank);
    }
}
