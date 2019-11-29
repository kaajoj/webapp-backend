using System.ComponentModel.DataAnnotations;

namespace Advantage.API.Models
{
    public class Wallet
    {
       [Key]
        public int idCrypto { get; set; }   
        public string Symbol { get; set; } 
        public string Price { get; set; }
        public string quantity { get; set; }
        public string sum { get; set; }
        public string startPrice { get; set; }
        public string changePrice { get; set; }
        public string alertUp { get; set; }
        public string alertDown { get; set; }
    }
}