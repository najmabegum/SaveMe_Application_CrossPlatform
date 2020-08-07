using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IGraphWeeklyDataService
    {
        //Task<List<SavingsWeekly>> GetGraphData(DataRequestModel dataRequestModel);
        Task<List<SavingsCategory>> GetGraphData(DataRequestModel dataRequestModel);
    }
}
