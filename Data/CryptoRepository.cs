using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Data
{
    public class CryptoRepository : Repository<Crypto>, ICryptoRepository
    {
        public CryptoRepository(ApiContext context) : base(context)
        {
        }
    }
}
