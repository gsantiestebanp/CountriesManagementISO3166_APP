using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Infrastructure;
using CountriesManagementISO3166_APP.Interfaces;
using CountriesManagementISO3166_APP.Services;
using FluentValidation.Results;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace CountriesManagementISO3166_APP.ViewModel
{
    public class AddCountryPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand AddCountryCmd { get; set; }

        public CountryDTO Country { get; set; }

        public AddCountryPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            Country = new CountryDTO();

            AddCountryCmd = new DelegateCommand(AddCountryExecute);
        }

        private async void AddCountryExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (CheckInternetAccess.CheckConnection())
                {
                    CountryDTOValidator countryValidador = new CountryDTOValidator();
                    ValidationResult validationResult = countryValidador.Validate(Country);

                    if (validationResult.IsValid)
                    {
                        CountryME country = new CountryME()
                        {
                            CountryId = Country.CountryId,
                            CommonName = Country.CommonName,
                            IsoName = Country.IsoName,
                            Alpha2Code = Country.Alpha2Code,
                            Alpha3Code = Country.Alpha3Code,
                            NumberSubdivisions = Country.NumberSubdivisions,
                            NumericCode = Country.NumericCode,
                            Observation = Country.Observation
                        };

                        await _userService.InsertCountry(country);
                        await _navigationService.NavigateAsync("CountryListPage");
                    }
                    else
                    {
                        IsBusy = false;
                        await _userDialogs.AlertAsync(ValidationErrors.Unfolds(validationResult));
                    }
                }
                else
                {
                    IsBusy = false;
                    await _userDialogs.AlertAsync("No tiene acceso a internet", "No hay internet");
                }
            }
            catch (Exception e)
            {
                IsBusy = false;
                _userDialogs.Toast(e.Message);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // Method intentionally left empty.
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            // Method intentionally left empty.
        }
    }
}
