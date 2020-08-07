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
    public class DailyExpenseService : BaseService, IDailyExpenseService
    {
        private readonly IGenericRepository _genericRepository;
        public DailyExpenseService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }
        public async Task<DailyExpenseReturnModel> AddUserDailyExpense(AddExpense addExpenseModel)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.AddDailyExpense
            };
            DailyExpenseReturnModel dailyExpenseReturnModel = await _genericRepository.PostAsync<AddExpense, DailyExpenseReturnModel>(uriBuilder.ToString(), addExpenseModel);
            return dailyExpenseReturnModel;
        }

        public async Task<DailyStatistics> GetWeekStatistics(int userID)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.WeekStatistics + userID
            };
            DailyStatistics dailyStatistics = await _genericRepository.GetAsync<DailyStatistics>(uriBuilder.ToString());
            return dailyStatistics;
        }
    }
}
