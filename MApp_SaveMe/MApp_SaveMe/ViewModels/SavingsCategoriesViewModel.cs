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
    public class SavingsCategoriesViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICategoriesDataService _categoriesDataService;
        private readonly IGraphWeeklyDataService _graphWeeklyDataService;
        private readonly IGraphMonthlyDataService _graphMonthlyDataService;

        public SavingsCategoriesViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService,
                                     ISettingsService settingsService,
                                     ICategoriesDataService categoriesDataService,
                                     IGraphWeeklyDataService graphWeeklyDataService,
                                     IGraphMonthlyDataService graphMonthlyDataService)
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            _categoriesDataService = categoriesDataService;
            _graphWeeklyDataService = graphWeeklyDataService;
            _graphMonthlyDataService = graphMonthlyDataService;
            //WeeklySavings = new ObservableCollection<SavingsWeekly>();
            WeeklySavings = new ObservableCollection<SavingsCategory>();
            MonthlySavings = new ObservableCollection<SavingsMonthly>();
            SavingsSummary = new SavingsSummary();
        }

        //private ObservableCollection<SavingsWeekly> _weeklySavings;

        //public ObservableCollection<SavingsWeekly> WeeklySavings
        //{
        //    get => _weeklySavings;
        //    set
        //    {
        //        _weeklySavings = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ObservableCollection<SavingsCategory> _weeklySavings;

        public ObservableCollection<SavingsCategory> WeeklySavings
        {
            get => _weeklySavings;
            set
            {
                _weeklySavings = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SavingsMonthly> _monthlySavings;

        public ObservableCollection<SavingsMonthly> MonthlySavings
        {
            get => _monthlySavings;
            set
            {
                _monthlySavings = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalSavingsAmount;

        public decimal TotalSavingsAmount
        {
            get => _totalSavingsAmount;
            set
            {
                _totalSavingsAmount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _categories;

        private ObservableCollection<string> _timePeriods;

        private string _categorySelected;

        private string _timePeriodSelected;
        public ICommand ShowGraph => new Command(OnShowGraph);

        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> TimePeriods
        {
            get => _timePeriods;
            set
            {
                _timePeriods = value;
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

        public string TimePeriodSelected
        {
            get => _timePeriodSelected;
            set
            {
                _timePeriodSelected = value;
                OnPropertyChanged();
            }
        }

        private SavingsSummary _savingsSummary;

        public SavingsSummary SavingsSummary
        {
            get => _savingsSummary;
            set
            {
                _savingsSummary = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            ObservableCollection<CategoryList> categoriesToDisplay = new ObservableCollection<CategoryList>(await _categoriesDataService.GetAllCategories());
            Categories = new ObservableCollection<string>(categoriesToDisplay.Select(e => e.CategoryName));
            TimePeriods = GlobalConstants.GraphTimePeriods;
            IsBusy = false;
        }

        public async void OnShowGraph()
        {
            IsBusy = true;
            DataRequestModel dataRequestModel = new DataRequestModel();
            dataRequestModel.CategoryName = CategorySelected;
            string selectedPeriod = string.Empty;
            switch(TimePeriodSelected)
            {
                case "Current Month":
                    selectedPeriod = GraphConstants.Period_CurrentMonth;
                    break;
                case "Previous Month":
                    selectedPeriod = GraphConstants.Period_PreviousMonth;
                    break;
                case "Last 6 Months":
                    selectedPeriod = GraphConstants.Period_HalfYearly;
                    break;
                case "Previous Year":
                    selectedPeriod = GraphConstants.Period_FullYear;
                    break;
            }
            if (selectedPeriod == GraphConstants.Period_CurrentMonth || selectedPeriod == GraphConstants.Period_PreviousMonth)
            {
                dataRequestModel.Type = GraphConstants.Type_Category;
                dataRequestModel.Period = selectedPeriod;
                dataRequestModel.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                //List<SavingsWeekly> weekSavings = new List<SavingsWeekly>();
                List<SavingsCategory> weekSavings = new List<SavingsCategory>();
                weekSavings = await _graphWeeklyDataService.GetGraphData(dataRequestModel);
                if(weekSavings.Any())
                {
                    //WeeklySavings = new ObservableCollection<SavingsWeekly>();
                    WeeklySavings = new ObservableCollection<SavingsCategory>();
                    weekSavings.Where(d=>d.Amount>0.00m).ToList().ForEach(e => WeeklySavings.Add(e));
                    IsBusy = false;
                    if(WeeklySavings.Any())
                    {
                        //SavingsSummary.SavingsWeekly = new ObservableCollection<SavingsWeekly>();
                        SavingsSummary.SavingsWeekly = new ObservableCollection<SavingsCategory>();
                        SavingsSummary.SavingsWeekly = WeeklySavings;
                        await _navigationService.NavigateToAsync<WeeklyGraphPageViewModel>(SavingsSummary);
                    }
                    else
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.GraphNoData, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.GraphNoData, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                
            }
            else if (selectedPeriod == GraphConstants.Period_HalfYearly || selectedPeriod == GraphConstants.Period_FullYear)
            {
                dataRequestModel.Type = GraphConstants.Type_Category;
                dataRequestModel.Period = selectedPeriod;
                dataRequestModel.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
                List<SavingsMonthly> monthlySavings = new List<SavingsMonthly>();
                monthlySavings = await _graphMonthlyDataService.GetGraphData(dataRequestModel);
                if(monthlySavings.Any())
                {
                    MonthlySavings = new ObservableCollection<SavingsMonthly>();
                    monthlySavings.ForEach(e => MonthlySavings.Add(e));
                    IsBusy = false;
                    if(MonthlySavings.Any())
                    {
                        SavingsSummary.SavingsMonthly = new ObservableCollection<SavingsMonthly>();
                        SavingsSummary.SavingsMonthly = MonthlySavings;
                        await _navigationService.NavigateToAsync<MonthlyGraphPageViewModel>(SavingsSummary);
                    }
                    else
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.GraphNoData, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }                    
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.GraphNoData, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                
            }
            else
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
            }
        }


    }
}
