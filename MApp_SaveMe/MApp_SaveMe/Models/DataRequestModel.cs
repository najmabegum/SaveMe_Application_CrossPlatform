using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class DataRequestModel
    {
        public string Type { get; set; }
        public string Period { get; set; }
        public int UserDataID { get; set; }
        public string CategoryName { get; set; }
    }
}
