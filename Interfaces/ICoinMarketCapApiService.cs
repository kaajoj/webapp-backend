using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using VSApi.Models;

namespace VSApi.Interfaces
{
    public interface ICoinMarketCapApiService
    {
        string CmcGet();
        Crypto CmcJsonParse(string cmcResponsej, int i);
        Task CmcSaveCryptosData(List<Crypto> cryptos);
    }
}