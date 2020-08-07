using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IGraphTotalSavingsService
    {
        Task<decimal> GetTotalSavings(int userDataID);
    }
}
