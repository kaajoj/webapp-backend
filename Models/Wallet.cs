using Org.BouncyCastle.Utilities.IO.Pem;

namespace VSApi.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int IdCrypto { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }
    
        public string OldPrice { get; set; }
        public string Price { get; set; }

        public string Change24h { get; set; }

        public string Change7d { get; set; }

        public int OwnFlag { get; set; }

        public string Quantity { get; set; }

        public string Sum { get; set; }

        public string AlertUp { get; set; }
        public string AlertDown { get; set; }       
        public string Change { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Crypto Crypto { get; set; }
    }
}