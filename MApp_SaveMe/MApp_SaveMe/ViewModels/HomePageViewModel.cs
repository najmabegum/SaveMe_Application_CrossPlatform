using MApp_SaveMe.Contracts;
using System.Threading.Tasks;

namespace MApp_SaveMe.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private HomePageMasterViewModel _menuViewModel;
        public HomePageViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            HomePageMasterViewModel menuViewModel)
            : base(connectionService, navigationService, dialogService)
        {
            _menuViewModel = menuViewModel;
        }

        public HomePageMasterViewModel MenuViewModel
        {
            get => _menuViewModel;
            set
            {
                _menuViewModel = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                _menuViewModel.InitializeAsync(data),
                _navigationService.NavigateToAsync<DailyExpenseViewModel>()
            );

            //await InitializeMenu(data);
            //await _navigationService.NavigateToAsync<DailyExpenseViewModel>();
            
        }

        public async Task InitializeMenu(object data)
        {
            await _menuViewModel.InitializeAsync(data);
        }
    }
}
