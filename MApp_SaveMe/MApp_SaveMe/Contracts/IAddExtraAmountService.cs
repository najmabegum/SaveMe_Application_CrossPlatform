using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IAddExtraAmountService
    {

        Task<decimal> GetMaximumAmountToAdd(AddExpense thresholdRequest);
        Task<bool> AddExtraAmount(AdditionalExpenseRequest expenseAddRequest);
    }
}
