using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class AdditionalExpenseRequest
    {        
        public bool IsFromCategory { get; set; }
        public string FromCategory { get; set; }        
        public string ToCategory { get; set; }
        public decimal AmountToAdd { get; set; }
        public int UserDataID { get; set; }
    }
}
