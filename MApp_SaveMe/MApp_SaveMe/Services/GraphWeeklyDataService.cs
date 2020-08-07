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
    public class GraphWeeklyDataService : BaseService, IGraphWeeklyDataService
    {
        private readonly IGenericRepository _genericRepository;
        public GraphWeeklyDataService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<SavingsCategory>> GetGraphData(DataRequestModel dataRequestModel)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.GetGraphData
            };
            List<SavingsCategory> weeklySavings = await _genericRepository.PostAsync<DataRequestModel, List<SavingsCategory>>(uriBuilder.ToString(), dataRequestModel);
            return weeklySavings;
        }
    }
}
