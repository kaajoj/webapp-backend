using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;

class WalletOperations
{
  Crypto crypto = new Crypto(); 
  public static Crypto calculateSum(Crypto crypto) {
      crypto.Sum = Math.Round((Convert.ToDouble(crypto.Price) * Convert.ToDouble(crypto.Quantity)),2).ToString(); 
      return crypto;
  }

}