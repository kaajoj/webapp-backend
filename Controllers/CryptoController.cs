using System;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CryptoController : Controller
    {
        private readonly ApiContext _ctx;
        private string response;
        private string checkId = null;
        
        public CryptoController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            response = CoinMarketCapAPI.cmcGet();
            dynamic jsonObj = JObject.Parse(response);
            try
                {
                Crypto cryptoTemp = new Crypto();
//                checkId = jsonObj["data"]["" + id + ""].ToString();
                checkId = jsonObj.SelectToken("$.data["+6+"].id").ToString();
                string cryptoName = jsonObj.SelectToken("$.data[" +6+ "].name").ToString();
                string cryptoSymbol = jsonObj.SelectToken("$.data[" +6+ "].symbol").ToString();
                string cryptoPrice = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +6+ "].quote.USD.price")),2).ToString();
                string cryptoChange_24h = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +6+ "].quote.USD.percent_change_24h")),2).ToString();
                string cryptoChange_7d = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +6+ "].quote.USD.percent_change_7d")),2).ToString();
                Console.WriteLine(checkId);
                Console.WriteLine(cryptoName);
                Console.WriteLine(cryptoSymbol);
                Console.WriteLine(cryptoPrice);
                Console.WriteLine(cryptoChange_24h);
                Console.WriteLine(cryptoChange_7d);
                    
                cryptoTemp.idCrypto = Convert.ToInt32(checkId);
                cryptoTemp.Name = cryptoName;
                cryptoTemp.Symbol = cryptoSymbol;
                cryptoTemp.Price = cryptoPrice;
                cryptoTemp.Change24h = cryptoChange_24h;
                cryptoTemp.Change7d = cryptoChange_7d;

                _ctx.Cryptos.Add(cryptoTemp);
                _ctx.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }


            var data = _ctx.Cryptos.OrderBy(c => c.idCrypto);

            return Ok(data);
        }

        // GET api/cryptos/5
        [HttpGet("{id}", Name = "GetCrypto")]
        public IActionResult Get(int id)
        {
            var crypto = _ctx.Cryptos.Find(id);
            return Ok(crypto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Crypto crypto)
        {
            if (crypto == null)
            {
                return BadRequest();
            }

            _ctx.Cryptos.Add(crypto);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCrypto", new { id = crypto.idCrypto }, crypto);
        }
    }
}