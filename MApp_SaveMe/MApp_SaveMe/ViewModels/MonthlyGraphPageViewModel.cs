using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class MonthlyGraphPageViewModel : ViewModelBase
    {
        public MonthlyGraphPageViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {
            SavingsMonthly = new List<SavingsMonthly>();            
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

        private List<SavingsMonthly> _savingsMonthly;

        public List<SavingsMonthly> SavingsMonthly
        {
            get => _savingsMonthly;
            set
            {
                _savingsMonthly = value;
                OnPropertyChanged();
            }
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public const string ChartTitle = "Savings - Monthly Basis";
        public const string ChartTitle_Primary = "Month";
        public const string ChartTitle_Secondary = "Amount";

        public ICommand HomeCommand => new Command(OnHomeCommand);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            SavingsSummary = (SavingsSummary)data;            
            SavingsMonthly = SavingsSummary.SavingsMonthly.ToList();
            Title = ChartTitle;
            IsBusy = false;
        }

        private async void OnHomeCommand()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                await _navigationService.NavigateToAsync<HomePageViewModel>();
            }
        }
    }
}
