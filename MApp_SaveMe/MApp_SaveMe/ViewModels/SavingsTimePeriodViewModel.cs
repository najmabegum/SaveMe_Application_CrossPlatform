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
    public class SavingsTimePeriodViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IGraphWeeklyDataService _graphWeeklyDataService;
        private readonly IGraphMonthlyDataService _graphMonthlyDataService;
        private readonly IGraphTotalSavingsService _graphTotalSavingsService;
        public SavingsTimePeriodViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService,
                                     ISettingsService settingsService,
                                     IGraphWeeklyDataService graphWeeklyDataService,
                                     IGraphMonthlyDataService graphMonthlyDataService,
                                     IGraphTotalSavingsService graphTotalSavingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            _graphWeeklyDataService = graphWeeklyDataService;
            _graphMonthlyDataService = graphMonthlyDataService;
            _graphTotalSavingsService = graphTotalSavingsService;
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

        public ICommand CurrentMonth => new Command(OnCurrentMonth);
        public ICommand PreviousMonth => new Command(OnPreviousMonth);
        public ICommand HalfYear => new Command(OnHalfYear);
        public ICommand OneYear => new Command(OnOneYear);
        public ICommand TotalSavings => new Command(OnTotalSavings);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = false;
        }

        public async void OnCurrentMonth()
        {
            IsBusy = true;
            DataRequestModel dataRequestModel = new DataRequestModel();
            dataRequestModel.Type = GraphConstants.Type_Amount;
            dataRequestModel.Period = GraphConstants.Period_CurrentMonth;
            dataRequestModel.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
            //List<SavingsWeekly> weeklySavings = new List<SavingsWeekly>();
            List<SavingsCategory> weeklySavings = new List<SavingsCategory>();
            weeklySavings = await _graphWeeklyDataService.GetGraphData(dataRequestModel);
            if(weeklySavings.Any())
            {
                //WeeklySavings = new ObservableCollection<SavingsWeekly>();
                WeeklySavings = new ObservableCollection<SavingsCategory>();
                weeklySavings.Where(d=>d.Amount>0.00m).ToList().ForEach(e => WeeklySavings.Add(e));
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

        public async void OnPreviousMonth()
        {
            IsBusy = true;
            DataRequestModel dataRequestModel = new DataRequestModel();
            dataRequestModel.Type = GraphConstants.Type_Amount;
            dataRequestModel.Period = GraphConstants.Period_PreviousMonth;
            dataRequestModel.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
            //List<SavingsWeekly> weeklySavings = new List<SavingsWeekly>();
            List<SavingsCategory> weeklySavings = new List<SavingsCategory>();
            weeklySavings = await _graphWeeklyDataService.GetGraphData(dataRequestModel);
            if(weeklySavings.Any())
            {
                //WeeklySavings = new ObservableCollection<SavingsWeekly>();
                WeeklySavings = new ObservableCollection<SavingsCategory>();
                weeklySavings.Where(d=>d.Amount>0.00m).ToList().ForEach(e => WeeklySavings.Add(e));
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

        public async void OnHalfYear()
        {
            IsBusy = true;
            DataRequestModel dataRequestModel = new DataRequestModel();
            dataRequestModel.Type = GraphConstants.Type_Amount;
            dataRequestModel.Period = GraphConstants.Period_HalfYearly;
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

        public async void OnOneYear()
        {
            IsBusy = true;
            DataRequestModel dataRequestModel = new DataRequestModel();
            dataRequestModel.Type = GraphConstants.Type_Amount;
            dataRequestModel.Period = GraphConstants.Period_FullYear;
            dataRequestModel.UserDataID = Convert.ToInt32(_settingsService.UserIdSetting);
            List<SavingsMonthly> monthlySavings = new List<SavingsMonthly>();
            monthlySavings = await _graphMonthlyDataService.GetGraphData(dataRequestModel);
            if(monthlySavings.Any())
            {
                MonthlySavings = new ObservableCollection<SavingsMonthly>();
                monthlySavings.Where(d=>d.Amount>0.00m).ToList().ForEach(e => MonthlySavings.Add(e));
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

        public async void OnTotalSavings()
        {
            IsBusy = true;
            if (!string.IsNullOrEmpty(_settingsService.UserIdSetting))
            {
                decimal totalSavings = await _graphTotalSavingsService.GetTotalSavings(Convert.ToInt32(_settingsService.UserIdSetting));
                IsBusy = false;
                if (totalSavings != 0.0m)
                {
                    string savings = totalSavings.ToString();
                    await _dialogService.ShowDialog(DialogMessagesConstants.Savings_Total + savings, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.Savings_Total_0, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }
            }
            else
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
            }
            await _navigationService.NavigateToAsync<HomePageViewModel>();
        }
    }
}
