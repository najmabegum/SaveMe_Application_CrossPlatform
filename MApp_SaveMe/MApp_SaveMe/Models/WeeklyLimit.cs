using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class WeeklyLimit
    {
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public int UserDataID { get; set; }
    }
}
