using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Models;

class WalletOperations
{
  Wallet crypto = new Wallet(); 
  public static Wallet calculateSum(Wallet crypto) {
      crypto.Sum = Math.Round((Convert.ToDouble(crypto.Price) * Convert.ToDouble(crypto.Quantity)),2).ToString(); 
      return crypto;
  }

}