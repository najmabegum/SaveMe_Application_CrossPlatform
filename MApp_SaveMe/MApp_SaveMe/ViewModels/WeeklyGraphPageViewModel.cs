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
    public class WeeklyGraphPageViewModel : ViewModelBase
    {
        public WeeklyGraphPageViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {
            //SavingsWeekly = new ObservableCollection<SavingsWeekly>();
            SavingsWeekly = new ObservableCollection<SavingsCategory>();
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

        //private ObservableCollection<SavingsWeekly> _savingsWeekly;

        //public ObservableCollection<SavingsWeekly> SavingsWeekly
        //{
        //    get => _savingsWeekly;
        //    set
        //    {
        //        _savingsWeekly = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ObservableCollection<SavingsCategory> _savingsWeekly;

        public ObservableCollection<SavingsCategory> SavingsWeekly
        {
            get => _savingsWeekly;
            set
            {
                _savingsWeekly = value;
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

        public const string ChartTitle = "Savings - Category Basis";
        public const string ChartTitle_Primary = "Category";
        public const string ChartTitle_Secondary = "Amount";

        public ICommand HomeCommand => new Command(OnHomeCommand);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            SavingsSummary = (SavingsSummary)data;
            SavingsWeekly = SavingsSummary.SavingsWeekly;
            Title = ChartTitle;
            IsBusy = false;
        }

        private async void OnHomeCommand()
        {
            if (_connectionService.IsConnected)
            {
                await _navigationService.NavigateToAsync<HomePageViewModel>();
            }
        }
    }
}
