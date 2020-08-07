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
    public class GraphMonthlyDataService : BaseService, IGraphMonthlyDataService
    {
        private readonly IGenericRepository _genericRepository;
        public GraphMonthlyDataService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<SavingsMonthly>> GetGraphData(DataRequestModel dataRequestModel)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.GetGraphData
            };
            List<SavingsMonthly> monthlySavings = await _genericRepository.PostAsync<DataRequestModel, List<SavingsMonthly>>(uriBuilder.ToString(), dataRequestModel);
            return monthlySavings;
        }
    }
}
