using System;
using VSApi.Models;

namespace VSApi.Services
{
  class WalletOperations
  { 
    public static Wallet CalculateSum(Wallet crypto) 
    {
        crypto.Sum = Math.Round((Convert.ToDouble(crypto.Price) * Convert.ToDouble(crypto.Quantity)),2).ToString(); 
        return crypto;
    }
    
    public static string CalculateAlerts(Wallet crypto) {   
        crypto.Change = Math.Round((((Convert.ToDouble(crypto.Price) / Convert.ToDouble(crypto.OldPrice)-1)*100)),2).ToString();
        return crypto.Change;
    }

    public static void GetAlerts(Wallet crypto) 
    {   
        if(Convert.ToDouble(crypto.Change) < -Convert.ToDouble(crypto.AlertDown)) 
        {
            string buyStr = "Price is below your alert(-" + crypto.AlertDown + "%)  -  buy  " + crypto.Name+"("+crypto.Symbol+")" + "";  
            Emails emails = new Emails();
            emails.prepareMessage(buyStr, crypto.Price, crypto.OldPrice, crypto.Change);
        }
        if(Convert.ToDouble(crypto.Change) > Convert.ToDouble(crypto.AlertUp)) 
        {
            string sellStr = "Price is above your alert(" + crypto.AlertUp + "%)  -  sell  " + crypto.Name+"("+crypto.Symbol+")" + "";
            Emails emails = new Emails();
            emails.prepareMessage(sellStr, crypto.Price, crypto.OldPrice, crypto.Change);
        }
    }

  }
}