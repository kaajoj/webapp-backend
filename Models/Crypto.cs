﻿using System.ComponentModel.DataAnnotations;

namespace VSApi.Models
{
    public class Crypto
    {
        [Key]
        public int Id { get; set; }
        public int IdCrypto { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }
    
        public string Price { get; set; }

        public string Change24h { get; set; }

        public string Change7d { get; set; }

        public int OwnFlag { get; set; }       
    }
}
