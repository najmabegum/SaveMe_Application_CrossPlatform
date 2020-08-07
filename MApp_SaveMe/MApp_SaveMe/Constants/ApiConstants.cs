using System;
using System.Text;

namespace MApp_SaveMe.Constants
{
    public static class ApiConstants
    {
        public const string BaseUri = "http://192.168.0.105:22222/";

        public const string GetAllCategories = "Categories/GetAllCategories";

        public const string UserRegistration = "UserRegistration/SaveUserRegistration";

        public const string Authenticate = "Login/Authenticate";

        public const string AddDailyExpense = "Expenses/DailyExpense";

        public const string WeekStatistics = "GetUserWeekStatistics/";

        public const string ResetWeeklyLimit = "WeeklyLimit/ResetLimit";

        public const string GetGraphData = "Graph/GetData";

        public const string GetTotalSavings = "Savings/GetTotalSavings/";

        public const string ChangePassword = "Login/ChangePassword";

        public const string UpdateUserData = "UpdateUserData/";

        public const string GetUserData = "GetUserData/";

        public const string AddAdditionalAmount = "AddAdditionalAmount/ResetLimit";

        public const string ThresholdRequest = "Expenses/GetAllowedAmount";
    }
}
