using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VSApi.Models;

namespace VSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private string _response;
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
            CoinMarketCapApi coinMarketCapApi = new CoinMarketCapApi();
            _response = coinMarketCapApi.cmcGet();
            dynamic jsonObj = JObject.Parse(_response);
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    Crypto cryptoTemp = new Crypto();
                    cryptoTemp = coinMarketCapApi.cmcJsonParse(jsonObj, i);
                    cryptos.Add(cryptoTemp);
                }

                foreach(var crypto in cryptos)
                {
                    if (!_ctx.Cryptos.Any())
                    {
                        _ctx.Cryptos.Add(crypto);   
                    } else {
                        var cryptoToUpdate = _ctx.Cryptos.First(c => c.IdCrypto == crypto.IdCrypto);
                        cryptoToUpdate.Price = crypto.Price;
                        cryptoToUpdate.Change24h = crypto.Change24h;
                        cryptoToUpdate.Change7d = crypto.Change7d;
                        _ctx.Cryptos.Update(cryptoToUpdate);
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

            return CreatedAtRoute("GetCrypto", new { id = crypto.IdCrypto }, crypto);
        }

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

            crypto.OwnFlag = flag;
            
            Console.WriteLine(crypto);
            _ctx.Cryptos.Update(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }

    }
}