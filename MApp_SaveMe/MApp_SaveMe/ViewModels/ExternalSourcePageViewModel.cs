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
    public class ExternalSourcePageViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICategoriesDataService _categoriesDataService;
        private readonly IAddExtraAmountService _addExtraAmountService;

        public ExternalSourcePageViewModel(IConnectionService connectionService,
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

        private string _categorySelected;

        public string CategorySelected
        {
            get => _categorySelected;
            set
            {
                _categorySelected = value;
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
        //public ICommand CategoryChangedCommand => new Command(OnCategoryChangedCommand);


        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            ObservableCollection<CategoryList> categoriesToDisplay = new ObservableCollection<CategoryList>(await _categoriesDataService.GetAllCategories());
            Categories = new ObservableCollection<string>(categoriesToDisplay.Select(e => e.CategoryName));
            MaxToBeAdded = ZeroAmount;
            Amount = ZeroAmount;
            string warningText = string.Empty;
            MaxToBeAddedWarning = "Please select a category";
            IsBusy = false;
        }

        //public async void OnCategoryChangedCommand()
        //{
        //    IsBusy = true;

        //    if (_connectionService.IsConnected)
        //    {
        //        AddExpense addExpense = new AddExpense();
        //        addExpense.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
        //        addExpense.Category = CategorySelected;
        //        decimal amountMax = await _addExtraAmountService.GetMaximumAmountToAdd(addExpense);
        //        MaxToBeAdded = amountMax;
        //        MaxToBeAddedWarning = "You cannot add more than " + amountMax.ToString() + " from the selcted category";
        //    }
        //}

        public async void OnSaveCommand()
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                AdditionalExpenseRequest additionalExpenseRequest = new AdditionalExpenseRequest();
                additionalExpenseRequest.IsFromCategory = false;
                additionalExpenseRequest.ToCategory = CategorySelected;
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
