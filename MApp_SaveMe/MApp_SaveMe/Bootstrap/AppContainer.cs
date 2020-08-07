using Autofac;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Repository;
using MApp_SaveMe.Services;
using MApp_SaveMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<DailyExpenseViewModel>();
            builder.RegisterType<HomePageViewModel>();
            builder.RegisterType<HomePageMasterViewModel>();
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<PersonalInformationPageViewModel>();
            builder.RegisterType<SavingsCategoriesViewModel>();
            builder.RegisterType<SavingsTimePeriodViewModel>();
            builder.RegisterType<SetExpensePageViewModel>();
            builder.RegisterType<WeeklyGraphPageViewModel>();
            builder.RegisterType<MonthlyGraphPageViewModel>();
            builder.RegisterType<UpdatePasswordPageViewModel>();
            builder.RegisterType<UserRegistrationViewModel>();
            builder.RegisterType<AddExtraExpensePageViewModel>();
            builder.RegisterType<ExternalSourcePageViewModel>();
            builder.RegisterType<FromCategoryPageViewModel>();

            //services - data
            builder.RegisterType<CategoryDataService>().As<ICategoriesDataService>();
            builder.RegisterType<DailyExpenseService>().As<IDailyExpenseService>();
            builder.RegisterType<WeeklyLimitService>().As<IWeeklyLimitService>();
            builder.RegisterType<GraphWeeklyDataService>().As<IGraphWeeklyDataService>();
            builder.RegisterType<GraphMonthlyDataService>().As<IGraphMonthlyDataService>();
            builder.RegisterType<GraphTotalSavingsService>().As<IGraphTotalSavingsService>();
            builder.RegisterType<UpdatePasswordService>().As<IUpdatePasswordService>();
            builder.RegisterType<PersonalInformationService>().As<IPersonalInformationService>();
            builder.RegisterType<AddExtraAmountService>().As<IAddExtraAmountService>();

            //services - general

            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            //builder.RegisterInstance(registrationRepository).As<IRegistrationRepository>();
            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

    }
}
