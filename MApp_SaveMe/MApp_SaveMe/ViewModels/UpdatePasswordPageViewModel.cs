using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class UpdatePasswordPageViewModel : ViewModelBase
    {
        private readonly IUpdatePasswordService _updatePasswordService;
        private readonly ISettingsService _settingsService;
        public UpdatePasswordPageViewModel(IConnectionService connectionService,
                                           INavigationService navigationService,
                                           IDialogService dialogService,
                                           IUpdatePasswordService updatePasswordService,
                                           ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _updatePasswordService = updatePasswordService;
            _settingsService = settingsService;
        }

        private string _passwordOne;

        private string _paswordTwo;

        public ICommand UpdateCommand => new Command(OnUpadteCommand);

        public string PaswordOne
        {
            get => _passwordOne;
            set
            {
                _passwordOne = value;
                OnPropertyChanged();
            }
        }

        public string PaswordTwo
        {
            get => _paswordTwo;
            set
            {
                _paswordTwo = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = false;
        }

        public async void OnUpadteCommand()
        {
            if (PaswordOne == PaswordTwo)
            {
                if (!string.IsNullOrEmpty(_settingsService.UserIdSetting))
                {
                    LoginModel loginModel = new LoginModel
                    {
                        UserDataID = Convert.ToInt32(_settingsService.UserIdSetting),
                        Password = PaswordOne
                    };
                    bool isSuccess = await _updatePasswordService.ChangePassword(loginModel);
                    if (isSuccess)
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.PasswordUpdate_Successful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }
                    else
                    {
                        await _dialogService.ShowDialog(DialogMessagesConstants.PasswordUpdate_Unsuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    }
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                }

            }
            else
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.PasswordUpdate_ErrorOne, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
            }
        }
    }
}
