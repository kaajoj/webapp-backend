using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VSApi.Models;

namespace VSApi.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly ApiContext _ctx;
        
        public WalletController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Wallet.OrderBy(c => c.Rank);

            return Ok(data);
        }

        // GET api/wallet/5
        [HttpGet("{id}", Name = "GetWallet")]
        public IActionResult Get(int id)
        {
            var crypto = _ctx.Wallet.Find(id);
            return Ok(crypto);
        }

        // api/wallet/
        [HttpPost]
        public IActionResult Post([FromBody] Wallet wallet)
        {
            if (wallet == null)
            {
                return BadRequest();
            }

            _ctx.Wallet.Add(wallet);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetWallet", new { id = wallet.Id }, wallet);
        }

        
        // api/wallet/delete/3
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _ctx.Wallet.Where(c => c.Rank == id).FirstOrDefault();

            if (crypto == null)
            {
                return NotFound();
            }

            _ctx.Wallet.Remove(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }


        // GET: wallet/edit/1/quantity/5
        [HttpGet("Edit/{id}/quantity/{quantity}")]
        public IActionResult EditQuantity(int? id, string quantity)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _ctx.Wallet.Where(c => c.Rank == id).FirstOrDefault();

            if (crypto == null)
            {
                return NotFound();
            }
            crypto.Quantity = quantity; 
            crypto = WalletOperations.calculateSum(crypto) ;
            Console.WriteLine(crypto);
            _ctx.Wallet.Update(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }


         // GET: wallet/edit/1/alertup/5
        [HttpGet("Edit/{id}/alertup/{alertup}")]
        public IActionResult setAlertUp(int? id, string alertup)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _ctx.Wallet.Where(c => c.Rank == id).FirstOrDefault();

            if (crypto == null)
            {
                return NotFound();
            }
            crypto.AlertUp = alertup; 
            Console.WriteLine(crypto);
            _ctx.Wallet.Update(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }

         // GET: wallet/edit/1/alertdown/5
        [HttpGet("Edit/{id}/alertdown/{alertdown}")]
        public IActionResult setAlertDown(int? id, string alertdown)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _ctx.Wallet.Where(c => c.Rank == id).FirstOrDefault();

            if (crypto == null)
            {
                return NotFound();
            }
            crypto.AlertDown = alertdown; 
            Console.WriteLine(crypto);
            _ctx.Wallet.Update(crypto);
            _ctx.SaveChanges();

            return Ok(crypto);
        }

        // GET: wallet/edit/prices
        [HttpGet("Edit/prices")]
        public IActionResult updatePrices()
        {
            List<Crypto> cryptoList = new List<Crypto>();
            List<Wallet> walletList = new List<Wallet>();

            cryptoList = _ctx.Cryptos.ToList();
            walletList = _ctx.Wallet.ToList();

            if (walletList == null)
            {
                return NotFound();
            }

            foreach(var cryptoWallet in walletList)
                    {
                        Crypto crypto = cryptoList.Find(c => c.IdCrypto == cryptoWallet.IdCrypto);
                        cryptoWallet.Price = crypto.Price;
                        cryptoWallet.Change24h = crypto.Change24h;
                        cryptoWallet.Change7d = crypto.Change7d;
                        cryptoWallet.Change = WalletOperations.calculateAlerts(cryptoWallet) ;
                        // Console.WriteLine(cryptoWallet.Change);
                        _ctx.Wallet.Update(cryptoWallet); 
                    }
             
            _ctx.SaveChanges();

            return Ok(walletList);
        }

        // GET: wallet/check/alerts
        [HttpGet("check/alerts")]
        public IActionResult checkAlerts()
        {
            List<Wallet> walletList = new List<Wallet>();
            walletList = _ctx.Wallet.ToList();

            foreach(var cryptoWallet in walletList)
                    {
                      WalletOperations.getAlerts(cryptoWallet) ; 
                    }
            
            return Ok();
        }

    }
}