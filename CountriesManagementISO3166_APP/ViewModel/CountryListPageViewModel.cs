using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Navigation;
using System;

namespace CountriesManagementISO3166_APP.ViewModel
{
    public class CountryListPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public CountryListPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            GetData();
        }

        private void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
