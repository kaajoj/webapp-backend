using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet GetWalletByRank(int? rank);
    }
}
