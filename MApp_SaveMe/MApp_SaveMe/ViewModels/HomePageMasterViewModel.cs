using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MApp_SaveMe.ViewModels
{
    public class HomePageMasterViewModel : ViewModelBase
    {
        private ObservableCollection<MainMenuItem> _menuItems;
        private readonly ISettingsService _settingsService;

        public HomePageMasterViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        public string WelcomeText => "Hello " + _settingsService.UserNameSetting;

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            if (menuItem != null && menuItem.MenuText == "Log out")
            {
                _settingsService.UserIdSetting = null;
                _settingsService.UserNameSetting = null;
                _navigationService.ClearBackStack();
            }

            var type = menuItem?.ViewModelToLoad;
            _navigationService.NavigateToAsync(type);
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Home",
                ViewModelToLoad = typeof(HomePageViewModel),
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Reset Expenses",
                ViewModelToLoad = typeof(SetExpensePageViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Add Additional Amount",
                ViewModelToLoad = typeof(AddExtraExpensePageViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Savings- By Time Period",
                ViewModelToLoad = typeof(SavingsTimePeriodViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Savings - By Category",
                ViewModelToLoad = typeof(SavingsCategoriesViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Update Personal Information",
                ViewModelToLoad = typeof(PersonalInformationPageViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Update Password",
                ViewModelToLoad = typeof(UpdatePasswordPageViewModel)
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Log out",
                ViewModelToLoad = typeof(LoginPageViewModel)                
            });
        }
    }
}
