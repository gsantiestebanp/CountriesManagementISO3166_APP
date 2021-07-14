using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Infrastructure;
using CountriesManagementISO3166_APP.Interfaces;
using CountriesManagementISO3166_APP.Services;
using FluentValidation.Results;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;

namespace CountriesManagementISO3166_APP.ViewModel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class AddCountryPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand AddCountryCmd { get; set; }
        public DelegateCommand EditCountryCmd { get; set; }

        public CountryDTO Country { get; set; }

        public bool IsEnableEditButton { get; set; }
        public bool IsEnableAddButton { get; set; }

        public AddCountryPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            IsEnableAddButton = true;
            IsEnableEditButton = false;

            Country = new CountryDTO();

            AddCountryCmd = new DelegateCommand(AddCountryExecute);
            EditCountryCmd = new DelegateCommand(EditCountryExecute);
        }

        private async void EditCountryExecute()
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

                        await _userService.UpdateCountry(country);
                        await _navigationService.GoBackAsync();
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
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                _userDialogs.Toast(e.Message);
            }
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
                        await _navigationService.GoBackAsync();
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
                IsBusy = false;
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
            if (parameters.ContainsKey("Country"))
            {
                Country = parameters["Country"] as CountryDTO;
                IsEnableEditButton = true;
                IsEnableAddButton = false;
                GetData();
            }
        }

        private void GetData()
        {
            Country = new CountryDTO()
            {
                CountryId = Country.CountryId,
                CommonName = Country.CommonName,
                Alpha2Code = Country.Alpha2Code,
                Alpha3Code = Country.Alpha3Code,
                IsoName = Country.IsoName,
                NumberSubdivisions = Country.NumberSubdivisions,
                NumericCode = Country.NumericCode,
                Observation = Country.Observation
            };
        }
    }
}
