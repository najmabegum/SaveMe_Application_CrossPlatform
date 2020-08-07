using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class AddExpense
    {
        public int UserDataID { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
