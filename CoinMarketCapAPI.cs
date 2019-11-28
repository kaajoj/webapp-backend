using System;
using System.Net;
using System.Web;

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

}