using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class DailyExpenseViewModel : ViewModelBase
    {
        private readonly ICategoriesDataService _categoriesDataService;
        private readonly ISettingsService _settingsService;
        private readonly IDailyExpenseService _dailyExpenseService;

        private ObservableCollection<string> _categories;

        private decimal _amount;

        private string _username;
        private string _greeting;
        private string _greetingDescription;
        private decimal _currentWeekTotalExpenditure;
        private decimal _currentWeekSavings;
        private decimal _currentWeekExpenses;
        private string _categorySelected;
        public ICommand SaveCommand => new Command(OnSaveCommand);

        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public decimal CurrentWeekTotalExpenditure
        {
            get => _currentWeekTotalExpenditure;
            set
            {
                _currentWeekTotalExpenditure = value;
                OnPropertyChanged();
            }
        }
        public decimal CurrentWeekSavings
        {
            get => _currentWeekSavings;
            set
            {
                _currentWeekSavings = value;
                OnPropertyChanged();
            }
        }
        public decimal CurrentWeekExpenses
        {
            get => _currentWeekExpenses;
            set
            {
                _currentWeekExpenses = value;
                OnPropertyChanged();
            }
        }
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public string CategorySelected
        {
            get => _categorySelected;
            set
            {
                _categorySelected = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                OnPropertyChanged();
            }
        }

        public string GreetingDescription
        {
            get => _greetingDescription;
            set
            {
                _greetingDescription = value;
                OnPropertyChanged();
            }
        }

        public DailyExpenseViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService,
                                     ICategoriesDataService categoriesDataService,
                                     ISettingsService settingsService,
                                     IDailyExpenseService dailyExpenseService)
            : base(connectionService, navigationService, dialogService)
        {
            _categoriesDataService = categoriesDataService;
            _settingsService = settingsService;
            _dailyExpenseService = dailyExpenseService;
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            UserName = _settingsService.UserNameSetting;
            Greeting = "Hello, " + UserName;
            GreetingDescription = "It's not about how much money you make, it's about how you save it!";
            int UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
            ObservableCollection<CategoryList> categoriesToDisplay = new ObservableCollection<CategoryList>(await _categoriesDataService.GetAllCategories());
            Categories = new ObservableCollection<string>(categoriesToDisplay.Select(e => e.CategoryName));
            Amount = 0.0m;

            DailyStatistics dailyStatistics = new DailyStatistics();
            dailyStatistics = await _dailyExpenseService.GetWeekStatistics(UserDataID);
            if(!dailyStatistics.IsWeekPlanSet)
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.DailyExpense_WeekPlanNotSet, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
            }
            CurrentWeekExpenses = dailyStatistics.CurrentWeekExpenses;
            CurrentWeekSavings = dailyStatistics.CurrentWeekSavings;
            CurrentWeekTotalExpenditure = dailyStatistics.CurrentWeekTotalExpenditure;

            IsBusy = false;
        }

        public async void OnSaveCommand()
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                AddExpense addExpenseModel = new AddExpense()
                {
                    Category = this.CategorySelected,
                    Amount = Convert.ToDecimal(this.Amount),
                    UserDataID = Convert.ToInt32(_settingsService.UserIdSetting)
                };
                DailyExpenseReturnModel dailyExpenseReturnModel = await _dailyExpenseService.AddUserDailyExpense(addExpenseModel);
                IsBusy = false;
                if (dailyExpenseReturnModel.isDatabaseUpdated && dailyExpenseReturnModel.hasThresholdReached)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.DailyExpenseSuccess_ThresholdReached, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                else if (dailyExpenseReturnModel.isDatabaseUpdated && !dailyExpenseReturnModel.hasThresholdReached)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.DailyExpenseSuccess_ThresholdNotReached, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                else if (!dailyExpenseReturnModel.isDatabaseUpdated)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.DailyExpense_Unsuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                CategorySelected = string.Empty;
                Amount = 0.0m;
                int UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                DailyStatistics dailyStatistics = new DailyStatistics();
                dailyStatistics = await _dailyExpenseService.GetWeekStatistics(UserDataID);
                if (!dailyStatistics.IsWeekPlanSet)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.DailyExpense_WeekPlanNotSet, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                CurrentWeekExpenses = dailyStatistics.CurrentWeekExpenses;
                CurrentWeekSavings = dailyStatistics.CurrentWeekSavings;
                CurrentWeekTotalExpenditure = dailyStatistics.CurrentWeekTotalExpenditure;

            }
        }
    }
}
