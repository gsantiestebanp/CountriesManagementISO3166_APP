using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Navigation;
using System;

namespace CountriesManagementISO3166_APP.ViewModel
{
    class AddSubdivisionPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public string CountryId { get; set; } 
        public AddSubdivisionPageViewModel(INavigationService navigationService, IUserService userService,
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
            if (parameters.ContainsKey("CountryId"))
            {
                CountryId = parameters["CountryId"].ToString();
            }
        }
    }
}
