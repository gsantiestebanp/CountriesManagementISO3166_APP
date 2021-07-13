using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Infrastructure;
using CountriesManagementISO3166_APP.Interfaces;
using FluentValidation.Results;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

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
                CountryDTOValidator countryValidador = new CountryDTOValidator();
                ValidationResult validationResult = countryValidador.Validate(Country);

                if (validationResult.IsValid)
                {

                }
                else
                {
                    IsBusy = false;
                    await _userDialogs.AlertAsync(ValidationErrors.Unfolds(validationResult));
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
