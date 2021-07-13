using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Interfaces;
using CountriesManagementISO3166_APP.Services;
using CountriesManagementISO3166_APP.ViewModel;
using CountriesManagementISO3166_APP.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace CountriesManagementISO3166_APP
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterInstance(UserDialogs.Instance);

            containerRegistry.RegisterInstance<IUserService>(new UserService());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CountryListPage, CountryListPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
