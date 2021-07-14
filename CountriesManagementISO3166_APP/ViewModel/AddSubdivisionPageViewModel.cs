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
    class AddSubdivisionPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand AddSubdivisionCmd { get; set; }
        public DelegateCommand EditSubdivisionCmd { get; set; }

        public SubdivisionDTO Subdivision { get; set; }

        public bool IsEnableEditButton { get; set; }
        public bool IsEnableAddButton { get; set; }

        public string CountryId { get; set; } 
        public AddSubdivisionPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            Subdivision = new SubdivisionDTO();

            AddSubdivisionCmd = new DelegateCommand(AddSubdivisionExecute);
            EditSubdivisionCmd = new DelegateCommand(EditSubdivisionExecute);
        }

        private async void EditSubdivisionExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (CheckInternetAccess.CheckConnection())
                {
                    SubdivisionDTOValidator subdivisionValidador = new SubdivisionDTOValidator();
                    ValidationResult validationResult = subdivisionValidador.Validate(Subdivision);

                    if (validationResult.IsValid)
                    {
                        SubdivisionME subdivision = new SubdivisionME()
                        {
                            CountryId = Subdivision.CountryId,
                            SubdivisionId = Subdivision.SubdivisionId,
                            Name = Subdivision.Name,
                            SubdivisionCode = Subdivision.SubdivisionCode
                        };

                        await _userService.UpdateSubdivision(subdivision);
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

        private async void AddSubdivisionExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (CheckInternetAccess.CheckConnection())
                {
                    SubdivisionDTOValidator subdivisionValidador = new SubdivisionDTOValidator();
                    ValidationResult validationResult = subdivisionValidador.Validate(Subdivision);

                    if (validationResult.IsValid)
                    {
                        SubdivisionME subdivision = new SubdivisionME()
                        {
                            CountryId = int.Parse(CountryId),
                            SubdivisionId = Subdivision.SubdivisionId,
                            Name = Subdivision.Name,
                            SubdivisionCode = Subdivision.SubdivisionCode                 
                        };

                        await _userService.InsertSubdivision(subdivision);
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
            if (parameters.ContainsKey("CountryId"))
            {
                CountryId = parameters["CountryId"].ToString();
            }

            if (parameters.ContainsKey("Subdivision"))
            {
                Subdivision = parameters["Subdivision"] as SubdivisionDTO;
                IsEnableEditButton = true;
                IsEnableAddButton = false;
                GetData();
            }
        }

        private void GetData()
        {
            Subdivision = new SubdivisionDTO()
            {
               CountryId = Subdivision.CountryId,
               Name = Subdivision.Name,
               SubdivisionCode = Subdivision.SubdivisionCode,
               SubdivisionId = Subdivision.SubdivisionId 
            };
        }
    }
}
