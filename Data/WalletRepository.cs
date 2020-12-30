using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Data
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApiContext context) : base(context) { }

        public ApiContext apiContext => _databaseContext as ApiContext;

        public Wallet GetWalletByRank(int? rank)
        {
            return apiContext.Wallet.FirstOrDefault(c => c.Rank == rank);
        }
    }
}
