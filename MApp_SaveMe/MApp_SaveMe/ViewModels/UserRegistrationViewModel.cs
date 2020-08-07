using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class UserRegistrationViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _gender;
        private string _age;
        private string _maritalStatus;
        private string _adultsInFamily;
        private string _kidsInFamily;
        private string _userName;
        private string _password;
        private bool _isConsentAccepted;

        public UserRegistrationViewModel(IConnectionService connectionService,
           INavigationService navigationService, IDialogService dialogService,
           IAuthenticationService authenticationService, ISettingsService settingsService)
           : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string MaritalStatus
        {
            get => _maritalStatus;
            set
            {
                _maritalStatus = value;
                OnPropertyChanged();
            }
        }

        public string AdultsInFamily
        {
            get => _adultsInFamily;
            set
            {
                _adultsInFamily = value;
                OnPropertyChanged();
            }
        }

        public string KidsInFamily
        {
            get => _kidsInFamily;
            set
            {
                _kidsInFamily = value;
                OnPropertyChanged();
            }
        }

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

        public bool IsConsentAccepted
        {
            get => _isConsentAccepted;
            set
            {
                _isConsentAccepted = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand => new Command(OnRegister);
        public ICommand TermsCommand => new Command(OnTerms);
        public ICommand LoginCommand => new Command(OnLogin);

        private async void OnRegister()
        {
            if (_connectionService.IsConnected)
            {
                if(_isConsentAccepted)
                {
                    var userRegistered = await
                    _authenticationService.Register(_firstName, _middleName, _lastName, _gender, Convert.ToInt32(_age),
                                                    _maritalStatus, Convert.ToInt32(_adultsInFamily), Convert.ToInt32(_kidsInFamily), _userName, _password);

                    if (userRegistered.IsAuthenticated)
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.RegistrationSuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                        _settingsService.UserIdSetting = userRegistered.User.Id.ToString();
                        await _navigationService.NavigateToAsync<LoginPageViewModel>();
                    }
                    else
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.RegistrationUnsuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                        await _navigationService.NavigateToAsync<LoginPageViewModel>();
                    }
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.TermsAndCondition, DialogMessagesConstants.Message, DialogMessagesConstants.OK);                    
                }
                
            }
        }

        private async void OnTerms()
        {
            if (_connectionService.IsConnected)
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.Terms, DialogMessagesConstants.ConsentTitle, DialogMessagesConstants.OK);
            }
        }

        private void OnLogin()
        {
            _navigationService.NavigateToAsync<LoginPageViewModel>();
        }
    }
}
