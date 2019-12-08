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
                    idCrypto = 8000,
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Rank = 1001,
                    Price = "4000",
                    Change24h = "30",
                    Change7d = "4",
                    ownFlag = 0,
                    Quantity = "0"
                },
                new Crypto {
                    idCrypto = 8001,
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Rank = 1002,
                    Price = "200",
                    Change24h = "5",
                    Change7d = "10",
                    ownFlag = 0,
                    Quantity = "0"
                },
                new Crypto {
                    idCrypto = 8002,
                    Name = "Litecoin",
                    Symbol = "LTC",
                    Rank = 1003,
                    Price = "40",
                    Change24h = "-2",
                    Change7d = "-10",
                    ownFlag = 0,
                    Quantity = "0"
                }

            };
        }
    }
}