using System;
using System.Net;
using System.Web;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Services
{
    public class CoinMarketCapApiService : ICoinMarketCapApiService
    {
        private const string ApiKey = "f742b5ad-230c-4dfe-b1dc-7fbe4ec51be4";

        public string CmcGet()
        {
            try
            {
                var response = MakeApiCall();
                return response;

            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public string MakeApiCall()
        {
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // queryString["start"] = "1";
            queryString["limit"] = "15";
            // queryString["convert"] = "USD,BTC";
            // queryString["convert"] = "BTC";

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", ApiKey);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(url.ToString());
        }

        public Crypto CmcJsonParse(dynamic jsonObj, int i)
        {
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
            cryptoTemp.OwnFlag = 0;

            return cryptoTemp;
        }

    }
}