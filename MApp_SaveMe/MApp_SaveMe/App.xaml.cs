using MApp_SaveMe.Bootstrap;
using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MApp_SaveMe
{
    public partial class App : Application
    {
        public App()
        {
            //License Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(GlobalConstants.Syncfusion_License);

            InitializeComponent();

            InitializeApp();

            InitializeNavigation();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
