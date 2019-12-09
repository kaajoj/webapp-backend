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

        // GET: api/crypto
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Cryptos.OrderBy(c => c.Rank);
            return Ok(data);
        }

        // GET api/crypto/5
        [HttpGet("{id}", Name = "GetCrypto")]
        public IActionResult Get(int id)
        {
            var crypto = _ctx.Cryptos.Find(id);
            return Ok(crypto);
        }

        [HttpGet("GetCmcApi")]
        public IActionResult GetCmcApi()
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
                        if (!_ctx.Cryptos.Any())
                        {
                            // _ctx.Cryptos.Add(cryptoTemp);
                            _ctx.Cryptos.Add(crypto);   
                        } else {
                            _ctx.Cryptos.Update(crypto);
                        }                                           
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

        // api/crypto/
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


        // Old function
        // GET: /api/crypto/getcryptowallet
        // [HttpGet("GetCryptoWallet", Name="GetCryptoWallet")]
        // public IActionResult GetCryptoWallet()
        // {
        //     var crypto = _ctx.Cryptos.Where(c => c.ownFlag == 1).OrderBy(c => c.Rank);
        //     return Ok(crypto);
        // }

        // Old function
        // GET: crypto/edit/1/own/5
        [HttpGet("Edit/{id}/own/{flag}")]
        public IActionResult Edit(int? id, int flag)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _ctx.Cryptos.Where(c => c.Rank == id).FirstOrDefault();

            if (crypto == null)
            {
                return NotFound();
            }

            crypto.ownFlag = flag;
            
            Console.WriteLine(crypto);
            _ctx.Cryptos.Update(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }

    }
}