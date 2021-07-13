using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CountriesManagementISO3166_APP.ViewModel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class CountryListPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand NavigateAddCountryCmd { get; set; }
        public DelegateCommand SearchCountryCmd { get; set; }
        public DelegateCommand<CountryDTO> NavigateToSelectedCountryCmd { get; set; }

        public ObservableCollection<CountryDTO> Countries { get; set; }
        public string SearchParameter { get; set; }

        public CountryListPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            Countries = new ObservableCollection<CountryDTO>();

            NavigateAddCountryCmd = new DelegateCommand(NavigateAddCountryExecute);
            NavigateToSelectedCountryCmd = new DelegateCommand<CountryDTO>(NavigateToSelectedCountryExecute);
            SearchCountryCmd = new DelegateCommand(CountrySearchExecute);

            MessagingCenter.Subscribe<object>(this, "Search", (sender) =>
            {
                CountrySearchExecute();
            });

            MessagingCenter.Subscribe<CountryDTO>(this, "Delete", (country) =>
            {
                DeleteCountryExecute(country);
            });
        }

        private async void DeleteCountryExecute(CountryDTO country)
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                CountryME countryME = new CountryME()
                {
                    CountryId = country.CountryId,
                    CommonName = country.CommonName,
                    Alpha2Code = country.Alpha2Code,
                    Alpha3Code = country.Alpha3Code,
                    IsoName = country.IsoName,
                    NumberSubdivisions = country.NumberSubdivisions,
                    NumericCode = country.NumericCode,
                    Observation = country.Observation
                };

                await _userService.DeleteCountry(countryME);
            }
            catch (Exception e)
            {
                IsBusy = false;
                _userDialogs.Toast(e.Message);
            }

            IsBusy = false;
            GetData();
        }

        private async void NavigateToSelectedCountryExecute(CountryDTO country)
        {
            if (IsBusy) return;
            IsBusy = true;

            var navigationParams = new NavigationParameters
            {
                { "Country", country },
            };

            IsBusy = false;
            await _navigationService.NavigateAsync("CountryDetailPage", navigationParams);
        }

        private void CountrySearchExecute()
        {
            var list = Countries.Where(c => c.Alpha2Code.Contains(SearchParameter));
            if (list != null)
                Countries = new ObservableCollection<CountryDTO>(list);
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
                    Countries.Clear();
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
