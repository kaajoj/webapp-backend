using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Models
{
    public class Crypto
    {
        [Key]
        public int idCrypto { get; set; }   
        public int Rank { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }
    
        public string Price { get; set; }

        public string Change24h { get; set; }

        public string Change7d { get; set; }

        public int ownFlag { get; set; }       
    }
}
