using System;
using System.Net;
using System.Web;
using Advantage.API.Models;

class CoinMarketCapAPI
{
  private static string API_KEY = "f742b5ad-230c-4dfe-b1dc-7fbe4ec51be4";

  internal static string cmcGet()
  {
    string response = "";

    try
    {
    response = makeAPICall();   
    Console.WriteLine(response);
    Console.WriteLine("COINMARKETCAP API makeAPICall");
    }
    catch (WebException e)
    {
    Console.WriteLine(e.Message);
    Console.WriteLine("COINMARKETCAP API EXCEPTION");
    }

    return response;
  }

  static string makeAPICall()
  {
    var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

    var queryString = HttpUtility.ParseQueryString(string.Empty);
    // queryString["start"] = "1";
    queryString["limit"] = "10";
    // queryString["convert"] = "USD,BTC";
    // queryString["convert"] = "BTC";


    URL.Query = queryString.ToString();

    var client = new WebClient();
    client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
    client.Headers.Add("Accepts", "application/json");
    return client.DownloadString(URL.ToString());

  }

 internal static Crypto cmcJsonParse(dynamic jsonObj, int i)
  {  
        Crypto cryptoTemp = new Crypto();
        // checkId = jsonObj["data"]["" + id + ""].ToString();
        string cryptoId = jsonObj.SelectToken("$.data["+i+"].id").ToString();
        string cryptoName = jsonObj.SelectToken("$.data[" +i+ "].name").ToString();
        string cryptoSymbol = jsonObj.SelectToken("$.data[" +i+ "].symbol").ToString();
        string cryptoRank = jsonObj.SelectToken("$.data[" +i+ "].cmc_rank").ToString();
        string cryptoPrice = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +i+ "].quote.USD.price")),2).ToString();
        string cryptoChange_24h = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +i+ "].quote.USD.percent_change_24h")),2).ToString();
        string cryptoChange_7d = Math.Round(Convert.ToDecimal(jsonObj.SelectToken("$.data[" +i+ "].quote.USD.percent_change_7d")),2).ToString();
        Console.WriteLine(cryptoId);
        Console.WriteLine(cryptoName);
        Console.WriteLine(cryptoSymbol);
        Console.WriteLine(cryptoRank);
        Console.WriteLine(cryptoPrice);
        Console.WriteLine(cryptoChange_24h);
        Console.WriteLine(cryptoChange_7d);
                    
        cryptoTemp.idCrypto = Convert.ToInt16(cryptoId);
        cryptoTemp.Name = cryptoName;
        cryptoTemp.Symbol = cryptoSymbol;
        cryptoTemp.Rank = Convert.ToInt16(cryptoRank);
        cryptoTemp.Price = cryptoPrice;
        cryptoTemp.Change24h = cryptoChange_24h;
        cryptoTemp.Change7d = cryptoChange_7d;
        cryptoTemp.ownFlag = 0;
        cryptoTemp.Quantity = "0";
  
    return cryptoTemp;
  }


}