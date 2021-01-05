using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using VSApi.Data;
using VSApi.Interfaces;
using VSApi.Models;
using VSApi.Services;

namespace VSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly ICoinMarketCapApiService _coinMarketCapApiService;

        public CryptoController(ICryptoRepository cryptoRepository, ICoinMarketCapApiService coinMarketCapApiService)
        {
            _cryptoRepository = cryptoRepository;
            _coinMarketCapApiService = coinMarketCapApiService;
        }

        // api/crypto
        [HttpGet]
        public IActionResult Get()
        {
            var data = _cryptoRepository.GetAll().OrderBy(c => c.Rank);
            return Ok(data);
        }

        // api/crypto/5
        [HttpGet("{id}", Name = "GetCrypto")]
        public IActionResult Get(int id)
        {
            var crypto = _cryptoRepository.Get(id);
            return Ok(crypto);
        }

        // api/crypto/getcmcapi
        [HttpGet("GetCmcApi")]
        public async Task<IActionResult> GetCmcApi()
        {
            var cryptos = new List<Crypto>();
            var cmcResponse = _coinMarketCapApiService.CmcGet();
            dynamic jsonObj = JObject.Parse(cmcResponse);
            try
            {
                for (var i = 0; i < 15; i++)
                {
                    var cryptoTemp = _coinMarketCapApiService.CmcJsonParse(jsonObj, i);
                    cryptos.Add(cryptoTemp);
                }

                await _coinMarketCapApiService.CmcSaveCryptosData(cryptos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(cryptos);
        }

        // api/crypto
        [HttpPost]
        public IActionResult Post([FromBody] Crypto crypto)
        {
            if (crypto == null)
            {
                return BadRequest();
            }

            _cryptoRepository.AddAsync(crypto);

            return CreatedAtRoute("GetCrypto", new { id = crypto.IdCrypto }, crypto);
        }

        // Old function
        // crypto/edit/1/own/5
        [HttpGet("Edit/{id}/own/{flag}")]
        public IActionResult Edit(int? id, int flag)
        {
            if (id == null)
            {
                return NotFound();
            }
            var crypto = _cryptoRepository.GetCryptoByRank(id);

            if (crypto == null)
            {
                return NotFound();
            }

            crypto.OwnFlag = flag;
            _cryptoRepository.UpdateAsync(crypto);

            return Ok(crypto);
        }

    }
}