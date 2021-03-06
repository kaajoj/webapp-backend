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
        private readonly ICryptoService _cryptoService;
        private readonly ICoinMarketCapApiService _coinMarketCapApiService;

        public CryptoController(ICryptoService cryptoService, ICoinMarketCapApiService coinMarketCapApiService)
        {
            _cryptoService = cryptoService;
            _coinMarketCapApiService = coinMarketCapApiService;
        }

        // api/crypto
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _cryptoService.GetAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // api/crypto/5
        [HttpGet("{id}", Name = "GetCrypto")]
        public IActionResult Get(int id)
        {
            var crypto = _cryptoService.Get(id);
            return Ok(crypto);
        }

        // api/crypto/getcmcapi
        [HttpGet("GetCmcApi")]
        public async Task<IActionResult> GetCmcApi()
        {
            var cryptos = new List<Crypto>();
            try
            {
                var cmcResponse = _coinMarketCapApiService.CmcGet();

                for (var i = 0; i < 15; i++)
                {
                    var cryptoTemp = _coinMarketCapApiService.CmcJsonParse(cmcResponse, i);
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
        public async Task<IActionResult> Post([FromBody] Crypto crypto)
        {
            if (crypto == null)
            {
                return BadRequest();
            }

            await _cryptoService.AddAsync(crypto);

            return CreatedAtRoute("GetCrypto", new { id = crypto.IdCrypto }, crypto);
        }

    }
}