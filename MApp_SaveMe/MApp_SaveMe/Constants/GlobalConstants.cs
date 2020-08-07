using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MApp_SaveMe.Constants
{
    public class GlobalConstants
    {
        public const string Syncfusion_License = "MjczMDMxQDMxMzgyZTMxMmUzMExET28xL2pZWFMvR1Awc21sc2xrdVB2MytXeERFZ1RTemJrMERYRnlkZlU9";
        
        public static ObservableCollection<string> MaritalStatus = new ObservableCollection<string>()
        {
            "Single", "Married", "Divorced"
        };

        public static ObservableCollection<string> Gender = new ObservableCollection<string>()
        {
            "Male", "Female", "Other"
        };

        public static ObservableCollection<string> AddExtraExpenseSource = new ObservableCollection<string>()
        {
            "From Another Category", "External Source"
        };

        public static ObservableCollection<string> GraphTimePeriods = new ObservableCollection<string>()
        {
            "Current Month",
            "Previous Month",
            "Last 6 Months",
            "Previous Year"
        };

    }
}
