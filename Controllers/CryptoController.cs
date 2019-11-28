using System;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CryptoController : Controller
    {
        private readonly ApiContext _ctx;
        private string response;
        
        public CryptoController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Crypto> cryptos = new List<Crypto>();
            response = CoinMarketCapAPI.cmcGet();
            dynamic jsonObj = JObject.Parse(response);
            try
                {
                    for (int i = 0; i < 10; i++)
                    {
                    Crypto cryptoTemp = new Crypto();
                    cryptoTemp = CoinMarketCapAPI.cmcJsonParse(jsonObj, i);
                    cryptos.Add(cryptoTemp);
                    }

                    foreach(var crypto in cryptos)
                    {
                        // _ctx.Cryptos.Add(cryptoTemp);
                        // _ctx.Cryptos.Add(crypto);                      
                        _ctx.Cryptos.Update(crypto);
                    }
                    _ctx.SaveChanges();
                    
               
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            var data = _ctx.Cryptos.OrderBy(c => c.Rank);
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