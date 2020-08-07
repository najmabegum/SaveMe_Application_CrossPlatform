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
    public class FromCategoryPageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICategoriesDataService _categoriesDataService;
        private readonly IAddExtraAmountService _addExtraAmountService;

        public FromCategoryPageViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService,
                                     ISettingsService settingsService,
                                     ICategoriesDataService categoriesDataService,
                                     IAddExtraAmountService addExtraAmountService)
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            _categoriesDataService = categoriesDataService;
            _addExtraAmountService = addExtraAmountService;
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private decimal _amount;

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _categories;

        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private string _fromCategorySelected;

        public string FromCategorySelected
        {
            get => _fromCategorySelected;
            set
            {
                _fromCategorySelected = value;
                OnPropertyChanged();
            }
        }

        private string _toCategorySelected;

        public string ToCategorySelected
        {
            get => _toCategorySelected;
            set
            {
                _toCategorySelected = value;
                OnPropertyChanged();
            }
        }

        private decimal _maxToBeAdded;

        public decimal MaxToBeAdded
        {
            get => _maxToBeAdded;
            set
            {
                _maxToBeAdded = value;
                OnPropertyChanged();
            }
        }

        private string _maxToBeAddedWarning;

        public string MaxToBeAddedWarning
        {
            get => _maxToBeAddedWarning;
            set
            {
                _maxToBeAddedWarning = value;
                OnPropertyChanged();
            }
        }

        public const decimal ZeroAmount = 0.00m;
        public ICommand SaveCommand => new Command(OnSaveCommand);
        public ICommand CategoryChangedCommandItemTapped => new Command(OnCategoryChangedCommandItemTapped);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            Description = "Transfer amount fromone category to another. The threshold amount will be dynamically shown to guide you the maximum amount that can be transferred";
            ObservableCollection<CategoryList> categoriesToDisplay = new ObservableCollection<CategoryList>(await _categoriesDataService.GetAllCategories());
            Categories = new ObservableCollection<string>(categoriesToDisplay.Select(e => e.CategoryName));
            MaxToBeAdded = ZeroAmount;
            Amount = ZeroAmount;
            string warningText = string.Empty;
            MaxToBeAddedWarning = "Please select a category";
            IsBusy = false;
        }

        public async void OnCategoryChangedCommandItemTapped(object menuItemTappedEventArgs)
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                AddExpense addExpense = new AddExpense();
                addExpense.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                addExpense.Category = FromCategorySelected;
                decimal amountMax = await _addExtraAmountService.GetMaximumAmountToAdd(addExpense);
                MaxToBeAdded = amountMax;
                MaxToBeAddedWarning = "You cannot add more than " + amountMax.ToString() + " from the selcted category";
            }
        }

        public async void OnSaveCommand(object menuItemTappedEventArgs)
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                if (Amount > MaxToBeAdded)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.AmountExceedError, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                else
                {
                    AdditionalExpenseRequest additionalExpenseRequest = new AdditionalExpenseRequest();
                    additionalExpenseRequest.IsFromCategory = true;
                    additionalExpenseRequest.FromCategory = FromCategorySelected;
                    additionalExpenseRequest.ToCategory = ToCategorySelected;
                    additionalExpenseRequest.AmountToAdd = Amount;
                    additionalExpenseRequest.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                    bool isAdded = await _addExtraAmountService.AddExtraAmount(additionalExpenseRequest);
                    if (isAdded)
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.AddExtra_Successful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }
                    else
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }

            }
        }

    }
}
