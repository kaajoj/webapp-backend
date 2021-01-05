using System;
using System.Net;
using System.Web;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface ICoinMarketCapApiService
    {
        string CmcGet();
        Crypto CmcJsonParse(dynamic jsonObj, int i);
    }
}