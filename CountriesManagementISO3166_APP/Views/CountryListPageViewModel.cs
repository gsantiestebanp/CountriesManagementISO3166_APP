using CountriesManagementISO3166_APP.Infrastructure;
using Prism.Navigation;

namespace CountriesManagementISO3166_APP.Views
{
    public class CountryListPageViewModel : ViewModelBase
    {
        public CountryListPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
    }
}
