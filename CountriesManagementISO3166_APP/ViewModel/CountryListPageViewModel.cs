using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace CountriesManagementISO3166_APP.ViewModel
{
    public class CountryListPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand NavigateAddCountryCmd { get; set; }

        public ObservableCollection<CountryDTO> Countries { get; set; }

        public CountryListPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            Countries = new ObservableCollection<CountryDTO>();

            NavigateAddCountryCmd = new DelegateCommand(NavigateAddCountryExecute);
        }

        private async void NavigateAddCountryExecute()
        {
            await _navigationService.NavigateAsync("AddCountryPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            GetData();
        }

        private async void GetData()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    var countries = await _userService.GetCountries();

                    foreach (var item in countries)
                    {
                        var countryDTO = new CountryDTO()
                        {
                            CountryId = item.CountryId,
                            CommonName = item.CommonName,
                            IsoName = item.IsoName,
                            Alpha2Code = item.Alpha2Code,
                            Alpha3Code = item.Alpha3Code,
                            NumberSubdivisions = item.NumberSubdivisions,
                            NumericCode = item.NumericCode,
                            Observation = item.Observation
                        };
                        Countries.Add(countryDTO);
                    }
                }
            }
            catch (Exception e)
            {
                IsBusy = false;
                _userDialogs.Toast(e.Message);
            }
        }
    }
}
