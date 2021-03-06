﻿using System;
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

        public ApiContext ApiContext => _databaseContext as ApiContext;

        public async Task AddRange(List<Crypto> cryptos)
        {
            await ApiContext.Cryptos.AddRangeAsync(cryptos);
            await _databaseContext.SaveChangesAsync();
        }

        public Crypto GetCryptoByIdCrypto(int idCrypto)
        {
            return ApiContext.Cryptos.FirstOrDefault(c => c.IdCrypto == idCrypto);
        }

        public async Task<Crypto> GetCryptoByRank(int? rank)
        {
            return await ApiContext.Cryptos.FirstOrDefaultAsync(c => c.Rank == rank);
        }
    }
}
