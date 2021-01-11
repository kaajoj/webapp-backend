using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSApi.Data;
using VSApi.Interfaces;
using VSApi.Models;
using VSApi.Services;

namespace VSApi.Controllers
{
    // [Authorize]
    // [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly IWalletOperationsService _walletOperationsService;

        public WalletController(
            IWalletRepository walletRepository, 
            ICryptoRepository cryptoRepository, 
            IWalletOperationsService walletOperationsService)
        {
            _walletRepository = walletRepository;
            _cryptoRepository = cryptoRepository;
            _walletOperationsService = walletOperationsService;
        }

        // api/wallet
        [HttpGet]
        public IActionResult Get()
        {
            var data = _walletRepository.GetAll().OrderBy(c => c.Rank);
            return Ok(data);
        }

        // api/wallet/5
        [HttpGet("{id}", Name = "GetWallet")]
        public IActionResult Get(int id)
        {
            var crypto = _walletRepository.Get(id);
            return Ok(crypto);
        }

        [HttpGet("GetWalletByUser/{id}")]
        public IActionResult GetWalletByUser(string id)
        {
            var crypto = _walletRepository.GetAll().Where(u => u.UserId == id);
            return Ok(crypto);
        }

        // api/wallet
        [HttpPost]
        public IActionResult Post([FromBody] Wallet wallet)
        {
            if (wallet == null)
            {
                return BadRequest();
            }

            _walletRepository.AddAsync(wallet);
            return CreatedAtRoute("GetWallet", new { id = wallet.Id }, wallet);
        }


        // api/wallet/delete/3
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _walletRepository.GetWalletByRank(id);

            if (crypto == null)
            {
                return NotFound();
            }

            await _walletRepository.RemoveAsync(crypto);
            return Ok(crypto);
        }


        // wallet/edit/1/quantity/5
        [HttpGet("Edit/{id}/quantity/{quantity}")]
        public IActionResult EditQuantity(int? id, string quantity)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _walletRepository.GetWalletByRank(id);

            if (crypto == null)
            {
                return NotFound();
            }

            crypto.Quantity = quantity;
            crypto = _walletOperationsService.CalculateSum(crypto);
            _walletRepository.UpdateAsync(crypto);
            return Ok(crypto);
        }


        // wallet/edit/1/alertup/5
        [HttpGet("Edit/{id}/alertup/{alertup}")]
        public IActionResult SetAlertUp(int? id, string alertup)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _walletRepository.GetWalletByRank(id);

            if (crypto == null)
            {
                return NotFound();
            }
            crypto.AlertUp = alertup;
            _walletRepository.UpdateAsync(crypto);
            return Ok(crypto);
        }

        // wallet/edit/1/alertdown/5
        [HttpGet("Edit/{id}/alertdown/{alertdown}")]
        public IActionResult SetAlertDown(int? id, string alertdown)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crypto = _walletRepository.GetWalletByRank(id);

            if (crypto == null)
            {
                return NotFound();
            }
            crypto.AlertDown = alertdown;
            _walletRepository.UpdateAsync(crypto);
            return Ok(crypto);
        }

        // wallet/edit/prices
        [HttpGet("Edit/prices")]
        public IActionResult UpdatePrices()
        {
            var cryptoList = _cryptoRepository.GetAll().ToList();
            var walletList = _walletRepository.GetAll().ToList();

            foreach (var cryptoWallet in walletList)
            {
                var crypto = cryptoList.Find(c => c.IdCrypto == cryptoWallet.IdCrypto);
                if (crypto != null)
                {
                    cryptoWallet.Price = crypto.Price;
                    cryptoWallet.Change24h = crypto.Change24h;
                    cryptoWallet.Change7d = crypto.Change7d;
                }

                cryptoWallet.Change = _walletOperationsService.CalculateAlerts(cryptoWallet);
                _walletRepository.UpdateAsync(cryptoWallet);
            }

            return Ok();
        }

        // wallet/check/alerts
        [HttpGet("check/alerts")]
        public IActionResult CheckAlerts()
        {
            var walletList = _walletRepository.GetAll().ToList();

            foreach (var cryptoWallet in walletList)
            {
                _walletOperationsService.GetAlerts(cryptoWallet);
            }

            return Ok();
        }

    }
}