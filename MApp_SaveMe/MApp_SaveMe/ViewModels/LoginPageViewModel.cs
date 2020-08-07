using MApp_SaveMe.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _userName;
        private string _password;

        public LoginPageViewModel(IConnectionService connectionService, ISettingsService settingsService,
            INavigationService navigationService,
            IAuthenticationService authenticationService,
            IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand RegisterCommand => new Command(OnRegister);
        public ICommand ChangePasswordCommand => new Command(OnChangePassword);

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private async void OnLogin()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                var authenticationResponse = await _authenticationService.Authenticate(UserName, Password);

                if (authenticationResponse.IsAuthenticated)
                {
                    // we store the Id to know if the user is already logged in to the application
                    _settingsService.UserIdSetting = authenticationResponse.User.Id.ToString();
                    _settingsService.UserNameSetting = authenticationResponse.User.FirstName;

                    IsBusy = false;
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }
            }
            else
            {
                await _dialogService.ShowDialog(
                    "This username/password combination isn't known",
                    "Error logging you in",
                    "OK");
            }
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<UserRegistrationViewModel>();
        }

        private void OnChangePassword()
        {
            _navigationService.NavigateToAsync<UpdatePasswordPageViewModel>();
        }
    }
}
