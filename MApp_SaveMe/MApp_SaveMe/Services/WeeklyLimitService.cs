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
    public class WeeklyLimitService : BaseService, IWeeklyLimitService
    {
        private readonly IGenericRepository _genericRepository;
        public WeeklyLimitService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> UpdateWeeklyLimit(List<WeeklyLimit> weeklyLimits)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.ResetWeeklyLimit
            };
            bool isLimitResetSuccessful = await _genericRepository.PostAsync<List<WeeklyLimit>, bool>(uriBuilder.ToString(), weeklyLimits);
            return isLimitResetSuccessful;
        }
    }
}
