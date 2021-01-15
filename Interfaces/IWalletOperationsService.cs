using System;
using System.Threading.Tasks;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface IWalletOperationsService
    {
        Wallet CalculateSum(Wallet crypto);
        string CalculateAlerts(Wallet crypto);
        Task GetAlerts(Wallet crypto);
    }
}