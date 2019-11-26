using System.Linq;
using Advantage.API.Models;
using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData()
        {
            if (!_ctx.Cryptos.Any())
            {
                SeedCryptos();
                _ctx.SaveChanges();
            }
        }

        private void SeedCryptos()
        {
            List<Crypto> cryptos = BuildCryptoList();

            foreach(var crypto in cryptos)
            {
                _ctx.Cryptos.Add(crypto);
            }
        }

        private List<Crypto> BuildCryptoList()
        {
            return new List<Crypto>()
            {
                new Crypto {
                    idCrypto = 1,
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Price = "4000",
                    Change24h = "30",
                    Change7d = "4"
                },
                new Crypto {
                    idCrypto = 2,
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Price = "200",
                    Change24h = "5",
                    Change7d = "10"
                },
                new Crypto {
                    idCrypto = 3,
                    Name = "Litecoin",
                    Symbol = "LTC",
                    Price = "40",
                    Change24h = "-2",
                    Change7d = "-10"
                }

            };
        }
    }
}