using System;
using System.Threading.Tasks;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Services
{
    public class WalletOperationsService : IWalletOperationsService
    {
        private readonly IEmailService _emailEmailService;

        public WalletOperationsService(IEmailService emailEmailService)
        {
            _emailEmailService = emailEmailService;
        }
        public Wallet CalculateSum(Wallet crypto)
        {
            crypto.Sum = Math.Round((Convert.ToDouble(crypto.Price) * Convert.ToDouble(crypto.Quantity)), 2).ToString();
            return crypto;
        }

        public string CalculateAlerts(Wallet crypto)
        {
            crypto.Change = Math.Round((((Convert.ToDouble(crypto.Price) / Convert.ToDouble(crypto.OldPrice) - 1) * 100)), 2).ToString();
            return crypto.Change;
        }

        public async Task GetAlerts(Wallet crypto)
        {
            if (Convert.ToDouble(crypto.Change) < -Convert.ToDouble(crypto.AlertDown))
            {
                var buyStr = $"Price is below your alert (-{crypto.AlertDown}%)  -  buy  {crypto.Name} ({crypto.Symbol})";
                var message = _emailEmailService.PrepareMessage(buyStr, crypto.Price, crypto.OldPrice, crypto.Change);
                await _emailEmailService.SendMessage(message);
            }
            if (Convert.ToDouble(crypto.Change) > Convert.ToDouble(crypto.AlertUp))
            {
                var sellStr = $"Price is above your alert ({crypto.AlertUp}%)  -  sell  {crypto.Name} ({crypto.Symbol})";
                var message = _emailEmailService.PrepareMessage(sellStr, crypto.Price, crypto.OldPrice, crypto.Change);
                await _emailEmailService.SendMessage(message);
            }
        }

    }
}