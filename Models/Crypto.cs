using System.ComponentModel.DataAnnotations;

namespace VSApi.Models
{
    public class Crypto : BaseModel
    {
        public int IdCrypto { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string Change24h { get; set; }
        public string Change7d { get; set; }
    }
}
