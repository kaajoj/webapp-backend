using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CryptoController : Controller
    {
        private readonly ApiContext _ctx;
        
        public CryptoController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CoinMarketCapAPI.cmcGet();
            
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