using System.ComponentModel.DataAnnotations;

namespace Advantage.API.Models
{
    public class Wallet
    {
        public int id { get; set; }   
        public int Rank { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }
    
        public string OldPrice { get; set; }
        public string Price { get; set; }

        public string Change24h { get; set; }

        public string Change7d { get; set; }

        public int ownFlag { get; set; }

        public string Quantity { get; set; }

        public string Sum { get; set; }

        public string AlertUp { get; set; }
        public string AlertDown { get; set; }       
    }
}