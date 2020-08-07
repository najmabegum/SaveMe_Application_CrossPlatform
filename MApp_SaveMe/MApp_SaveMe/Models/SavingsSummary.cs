using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MApp_SaveMe.Models
{
    public class SavingsSummary
    {
        //public ObservableCollection<SavingsWeekly> SavingsWeekly = new ObservableCollection<SavingsWeekly>();
        public ObservableCollection<SavingsCategory> SavingsWeekly = new ObservableCollection<SavingsCategory>();

        public ObservableCollection<SavingsMonthly> SavingsMonthly = new ObservableCollection<SavingsMonthly>();
    }
}
