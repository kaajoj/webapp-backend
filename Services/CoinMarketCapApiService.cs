using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Services
{
    public class CoinMarketCapApiService : ICoinMarketCapApiService
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly IConfiguration _configuration;

        public CoinMarketCapApiService(ICryptoRepository cryptoRepository, IConfiguration configuration)
        {
            _cryptoRepository = cryptoRepository;
            _configuration = configuration;
        }

        public string CmcGet()
        {
            var apiKey = _configuration["CMCApiKey"];
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // queryString["start"] = "1";
            queryString["limit"] = "15";
            // queryString["convert"] = "USD,BTC";
            // queryString["convert"] = "BTC";

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
            client.Headers.Add("Accepts", "application/json");
            var response = client.DownloadString(url.ToString());

            return response;
        }

        public Crypto CmcJsonParse(string cmcResponse, int i)
        {
            dynamic jsonObj = JObject.Parse(cmcResponse);

            var cryptoTemp = new Crypto();
            // checkId = jsonObj["data"]["" + id + ""].ToString();
            string cryptoId = jsonObj.SelectToken("$.data[" + i + "].id").ToString();
            string cryptoName = jsonObj.SelectToken("$.data[" + i + "].name").ToString();
            string cryptoSymbol = jsonObj.SelectToken("$.data[" + i + "].symbol").ToString();
            string cryptoRank = jsonObj.SelectToken("$.data[" + i + "].cmc_rank").ToString();
            string cryptoPrice = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" + i + "].quote.USD.price")), 2).ToString();
            string cryptoChange24H = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" + i + "].quote.USD.percent_change_24h")), 2).ToString();
            string cryptoChange7D = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" + i + "].quote.USD.percent_change_7d")), 2).ToString();

            cryptoTemp.IdCrypto = Convert.ToInt16(cryptoId);
            cryptoTemp.Name = cryptoName;
            cryptoTemp.Symbol = cryptoSymbol;
            cryptoTemp.Rank = Convert.ToInt16(cryptoRank);
            cryptoTemp.Price = cryptoPrice;
            cryptoTemp.Change24h = cryptoChange24H;
            cryptoTemp.Change7d = cryptoChange7D;

            return cryptoTemp;
        }

        public async Task CmcSaveCryptosData(List<Crypto> cryptos)
        {
            if (!_cryptoRepository.GetAll().Any())
            {
                await _cryptoRepository.AddRange(cryptos);
            }
            else
            {
                foreach (var crypto in cryptos)
                {
                    var cryptoToUpdate = _cryptoRepository.GetCryptoByIdCrypto(crypto.IdCrypto);
                    if (cryptoToUpdate != null)
                    {   
                        cryptoToUpdate.Price = crypto.Price;
                        cryptoToUpdate.Change24h = crypto.Change24h;
                        cryptoToUpdate.Change7d = crypto.Change7d;
                        await _cryptoRepository.UpdateAsync(cryptoToUpdate);
                    }
                    else
                    {
                        await _cryptoRepository.AddAsync(crypto);
                    }
                }
            }
        }
    }
}