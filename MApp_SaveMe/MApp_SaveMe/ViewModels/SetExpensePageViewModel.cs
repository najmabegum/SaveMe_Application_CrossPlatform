using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class SetExpensePageViewModel : ViewModelBase
    {
        private readonly ICategoriesDataService _categoriesDataService;
        private readonly ISettingsService _settingsService;
        private readonly IWeeklyLimitService _weeklyLimitService;

        public SetExpensePageViewModel(IConnectionService connectionService,
                                       INavigationService navigationService,
                                       IDialogService dialogService,
                                       ICategoriesDataService categoriesDataService,
                                       ISettingsService settingsService,
                                       IWeeklyLimitService weeklyLimitService)
            : base(connectionService, navigationService, dialogService)
        {
            _categoriesDataService = categoriesDataService;
            _settingsService = settingsService;
            _weeklyLimitService = weeklyLimitService;
            ExpenseList = new ObservableCollection<SetExpenseModel>();
        }

        public const string InitialAmount = "0.0";

        private ObservableCollection<SetExpenseModel> _expenseList;

        public ObservableCollection<SetExpenseModel> ExpenseList
        {
            get => _expenseList;
            set
            {
                _expenseList = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand => new Command(OnSaveCommand);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            ObservableCollection<CategoryList> categoriesToDisplay = new ObservableCollection<CategoryList>(await _categoriesDataService.GetAllCategories());
            foreach (var item in categoriesToDisplay)
            {
                SetExpenseModel expense = new SetExpenseModel();
                expense.CategoryName = item.CategoryName;
                expense.Amount = InitialAmount;
                ExpenseList.Add(expense);
            }
            IsBusy = false;
        }

        public async void OnSaveCommand()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                List<WeeklyLimit> weeklyLimits = new List<WeeklyLimit>();
                foreach (var expense in ExpenseList)
                {
                    WeeklyLimit limit = new WeeklyLimit();
                    limit.CategoryName = expense.CategoryName;
                    limit.Amount = Convert.ToDecimal(expense.Amount);
                    limit.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                    weeklyLimits.Add(limit);
                }
                bool isSuccess = await _weeklyLimitService.UpdateWeeklyLimit(weeklyLimits);
                IsBusy = false;
                if (isSuccess)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.ResetExpense_Successful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.ResetExpense_Unsuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }
                
            }
        }
    }
}
