using MApp_SaveMe.Contracts;
using MApp_SaveMe.ViewModels;
using MApp_SaveMe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MApp_SaveMe.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            if (_authenticationService.IsUserAuthenticated())
            {
                await NavigateToAsync<HomePageViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginPageViewModel>();
            }
        }

        public async Task ClearBackStack()
        {
            await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is HomePage mainPage)
            {
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is HomePage mainPage)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public async Task PopToRootAsync()
        {
            if (CurrentApplication.MainPage is HomePage mainPage)
            {
                await mainPage.Detail.Navigation.PopToRootAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);

            if (page is HomePage || page is UserRegistration)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is LoginPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is HomePage)
            {
                var mainPage = CurrentApplication.MainPage as HomePage;

                if (mainPage.Detail is HomePageDetail navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new HomePageDetail(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as HomePageDetail;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new HomePageDetail(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(LoginPageViewModel), typeof(LoginPage));
            _mappings.Add(typeof(HomePageViewModel), typeof(HomePage));
            _mappings.Add(typeof(DailyExpenseViewModel), typeof(DailyExpense));
            _mappings.Add(typeof(PersonalInformationPageViewModel), typeof(PersonalInformationPage));
            _mappings.Add(typeof(SavingsCategoriesViewModel), typeof(SavingsCategories));
            _mappings.Add(typeof(SavingsTimePeriodViewModel), typeof(SavingsTimePeriod));
            _mappings.Add(typeof(WeeklyGraphPageViewModel), typeof(WeeklyGraphPage));
            _mappings.Add(typeof(MonthlyGraphPageViewModel), typeof(MonthlyGraphPage));
            _mappings.Add(typeof(SetExpensePageViewModel), typeof(SetExpensePage));
            _mappings.Add(typeof(UserRegistrationViewModel), typeof(UserRegistration));
            _mappings.Add(typeof(UpdatePasswordPageViewModel), typeof(UpdatePasswordPage));
            _mappings.Add(typeof(AddExtraExpensePageViewModel), typeof(AddExtraExpensePage));
            _mappings.Add(typeof(ExternalSourcePageViewModel), typeof(ExternalSourcePage));
            _mappings.Add(typeof(FromCategoryPageViewModel), typeof(FromCategoryPage));

        }
    }
}
