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
    public class AddExtraAmountService : BaseService, IAddExtraAmountService
    {
        private readonly IGenericRepository _genericRepository;
        public AddExtraAmountService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> AddExtraAmount(AdditionalExpenseRequest expenseAddRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.AddAdditionalAmount
            };
            bool isAmountAdded = await _genericRepository.PostAsync<AdditionalExpenseRequest, bool>(uriBuilder.ToString(), expenseAddRequest);
            return isAmountAdded;
        }

        public async Task<decimal> GetMaximumAmountToAdd(AddExpense thresholdRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.ThresholdRequest
            };
            decimal amount = await _genericRepository.PostAsync<AddExpense, decimal>(uriBuilder.ToString(), thresholdRequest);
            return amount;
        }
    }
}