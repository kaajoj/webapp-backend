using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Models;

namespace App.API
{
  class WalletOperations
  {
    Wallet crypto = new Wallet(); 
    public static Wallet calculateSum(Wallet crypto) {
        crypto.Sum = Math.Round((Convert.ToDouble(crypto.Price) * Convert.ToDouble(crypto.Quantity)),2).ToString(); 
        return crypto;
    }
    
    public static string calculateAlerts(Wallet crypto) {   
        crypto.Change = Math.Round((((Convert.ToDouble(crypto.Price)/Convert.ToDouble(crypto.OldPrice)-1)*100)),2).ToString();
        
          return crypto.Change;
    }

      public static void getAlerts(Wallet crypto) {   
        
          if(Convert.ToDouble(crypto.Change)<-Convert.ToDouble(crypto.AlertDown)) {
            string buyStr = "Price is below your alert(-" + crypto.AlertDown + "%)  -  buy  " + crypto.Name+"("+crypto.Symbol+")" + "";  
            Emails emails = new Emails();
            emails.prepareMessage(buyStr, crypto.Price, crypto.OldPrice, crypto.Change);
          }
          if(Convert.ToDouble(crypto.Change)>Convert.ToDouble(crypto.AlertUp)) {
            string sellStr = "Price is above your alert(" + crypto.AlertUp + "%)  -  sell  " + crypto.Name+"("+crypto.Symbol+")" + "";
            Emails emails = new Emails();
            emails.prepareMessage(sellStr, crypto.Price, crypto.OldPrice, crypto.Change);
          }

    }

  }

}