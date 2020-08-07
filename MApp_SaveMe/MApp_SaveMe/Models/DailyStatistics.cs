using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class DailyStatistics
    {
        public decimal CurrentWeekTotalExpenditure { get; set; }

        public decimal CurrentWeekSavings { get; set; }

        public decimal CurrentWeekExpenses { get; set; }

        public bool IsWeekPlanSet { get; set; }
    }
}
