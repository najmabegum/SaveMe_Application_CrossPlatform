using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class AddExtraExpensePageViewModel : ViewModelBase
    {        
        public AddExtraExpensePageViewModel(IConnectionService connectionService,
                                     INavigationService navigationService,
                                     IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {

        }

        public ICommand FromAnotherCategory => new Command(OnAnotherCategory);
        public ICommand FromExternalSource => new Command(OnExternalSource);

        public async void OnAnotherCategory()
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                await _navigationService.NavigateToAsync<FromCategoryPageViewModel>();
            }
        }

        public async void OnExternalSource()
        {
            IsBusy = true;

            if (_connectionService.IsConnected)
            {
                await _navigationService.NavigateToAsync<ExternalSourcePageViewModel>();
            }
        }

    }
}
