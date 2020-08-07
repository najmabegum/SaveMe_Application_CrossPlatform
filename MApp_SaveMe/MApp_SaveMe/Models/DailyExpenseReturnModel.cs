using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class DailyExpenseReturnModel
    {
        public bool isDatabaseUpdated { get; set; }
        public bool hasThresholdReached { get; set; }
    }
}
