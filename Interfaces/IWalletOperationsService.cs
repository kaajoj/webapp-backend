using System;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface IWalletOperationsService
    {
        Wallet CalculateSum(Wallet crypto);
        string CalculateAlerts(Wallet crypto);
        void GetAlerts(Wallet crypto);
    }
}