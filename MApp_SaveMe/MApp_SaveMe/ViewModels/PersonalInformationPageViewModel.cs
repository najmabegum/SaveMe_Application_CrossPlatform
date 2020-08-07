using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class PersonalInformationPageViewModel : ViewModelBase
    {        
        private readonly ISettingsService _settingsService;
        private readonly IPersonalInformationService _personalInformationService;

        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _gender;
        private string _age;
        private string _maritalStatus;
        private string _adultsInFamily;
        private string _kidsInFamily;
        private bool _isMale;
        private bool _isFemale;

        public PersonalInformationPageViewModel(IConnectionService connectionService,
                                         INavigationService navigationService, 
                                         IDialogService dialogService,
                                         ISettingsService settingsService,
                                         IPersonalInformationService personalInformationService)
           : base(connectionService, navigationService, dialogService)
        {            
            _settingsService = settingsService;
            _personalInformationService = personalInformationService;
            User = new User();
        }

        private ObservableCollection<string> _genderDropdown;

        public ObservableCollection<string> GenderDropdown
        {
            get => _genderDropdown;
            set
            {
                _genderDropdown = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _maritalStatusDropdown;

        public ObservableCollection<string> MaritalStatusDropdown
        {
            get => _maritalStatusDropdown;
            set
            {
                _maritalStatusDropdown = value;
                OnPropertyChanged();
            }
        }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
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

        public bool IsMale
        {
            get => _isMale;
            set
            {
                _isMale = value;
                OnPropertyChanged();
            }
        }

        public bool IsFemale
        {
            get => _isFemale;
            set
            {
                _isFemale = value;
                OnPropertyChanged();
            }
        }


        public ICommand UpdateCommand => new Command(OnUpdate);

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            GenderDropdown = GlobalConstants.Gender;
            MaritalStatusDropdown = GlobalConstants.MaritalStatus;

            if (!string.IsNullOrEmpty(_settingsService.UserIdSetting))
            {
                int userDataID = Convert.ToInt32(_settingsService.UserIdSetting);                
                User = await _personalInformationService.FetchUser(userDataID);
                if (User != null)
                {
                    FirstName = User.FirstName;
                    MiddleName = User.MiddleName;
                    LastName = User.LastName;
                    Gender = User.Gender;
                    Age = User.Age.ToString();
                    MaritalStatus = User.MaritalStatus;
                    AdultsInFamily = User.NumberMembersInFamily.ToString();
                    KidsInFamily = User.NumberKidsInFamily.ToString();
                }
                if(Gender == "Male")
                {
                    IsMale = true;
                    IsFemale = false;
                }
                else
                {
                    IsFemale = true;
                    IsMale = false;
                }
                IsBusy = false;
            }
            else
            {
                await _dialogService.ShowDialog(DialogMessagesConstants.Retry, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
            }
                        
            
        }

        private async void OnUpdate()
        {
            if (_connectionService.IsConnected)
            {
                UserData user = new UserData();
                user.FirstName = _firstName;
                user.MiddleName = _middleName;
                user.LastName = _lastName;
                user.Gender = _gender;
                user.Age = Convert.ToInt32(_age);
                user.MaritalStatus = _maritalStatus;
                user.NumberMembersInFamily = Convert.ToInt32(_adultsInFamily);
                user.NumberKidsInFamily= Convert.ToInt32(_kidsInFamily);
                int.TryParse(_settingsService.UserIdSetting,out int userDataID);
                user.UserDataID = userDataID;
                bool isUserUpdated = await _personalInformationService.UpdateUserData(userDataID, user);               
                if (isUserUpdated)
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.PersonalInfo_Successful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);                    
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }
                else
                {
                    await _dialogService.ShowDialog(DialogMessagesConstants.PersonalInfo_Unsuccessful, DialogMessagesConstants.Message, DialogMessagesConstants.OK);
                    await _navigationService.NavigateToAsync<HomePageViewModel>();
                }
            }
        }
    }
}
