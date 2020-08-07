using Akavache;
using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Services
{
    public class GraphTotalSavingsService : BaseService, IGraphTotalSavingsService
    {
        private readonly IGenericRepository _genericRepository;
        public GraphTotalSavingsService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<decimal> GetTotalSavings(int userDataID)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.GetTotalSavings + userDataID
            };
            decimal totalSavings = await _genericRepository.GetAsync<decimal>(uriBuilder.ToString());
            return totalSavings;
        }
    }
}
