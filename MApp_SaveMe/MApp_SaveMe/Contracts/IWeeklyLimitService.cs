using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IWeeklyLimitService
    {
        Task<bool> UpdateWeeklyLimit(List<WeeklyLimit> weeklyLimits);
    }
}
