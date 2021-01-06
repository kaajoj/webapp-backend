using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSApi.Interfaces;
using VSApi.Models;

namespace VSApi.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoService(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository;
        }
        public IEnumerable<Crypto> GetAll()
        {
            return _cryptoRepository.GetAll().OrderBy(c => c.Rank);
        }

        public Crypto Get(int id)
        {
            return _cryptoRepository.Get(id);
        }

        public async Task<Crypto> AddAsync(Crypto crypto)
        {
            return await _cryptoRepository.AddAsync(crypto);
        }
    }
}
