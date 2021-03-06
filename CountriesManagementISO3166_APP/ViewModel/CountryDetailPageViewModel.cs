using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CountriesManagementISO3166_APP.ViewModel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class CountryDetailPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand NavigateAddSubdivisionCmd { get; set; }

        public CountryDTO Country { get; set; }
        public ObservableCollection<SubdivisionDTO> Subdivisions { get; set; }

        public CountryDetailPageViewModel(INavigationService navigationService, IUserService userService,
            IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;

            Country = new CountryDTO();
            Subdivisions = new ObservableCollection<SubdivisionDTO>();

            Subdivisions = new ObservableCollection<SubdivisionDTO>();

            NavigateAddSubdivisionCmd = new DelegateCommand(NavigateAddSubdivisionExecute);

            MessagingCenter.Subscribe<SubdivisionDTO>(this, "Edit", (subdivision) =>
            {
                EditSubdivisionExecute(subdivision);
            });

        }

        private async void EditSubdivisionExecute(SubdivisionDTO subdivision)
        {
            if (IsBusy) return;
            IsBusy = true;

            var navigationParams = new NavigationParameters
            {
                { "Subdivision", subdivision },
            };

            IsBusy = false;
            await _navigationService.NavigateAsync("AddSubdivisionPage", navigationParams);
        }

        private async void NavigateAddSubdivisionExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            var navigationParams = new NavigationParameters
            {
                { "CountryId", Country.CountryId },
            };

            IsBusy = false;
            await _navigationService.NavigateAsync("AddSubdivisionPage", navigationParams);
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
                GetData();
            }
        }

        private async void GetData()
        {
            try
            {
                var subdivisions = await _userService.GetSubdivisionsByCountryId(Country.CountryId);

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

                if (subdivisions != null)
                {
                    foreach (var item in subdivisions)
                    {
                        var subdivision = new SubdivisionDTO
                        {
                            CountryId = item.CountryId,
                            Name = item.Name,
                            SubdivisionCode = item.SubdivisionCode,
                            SubdivisionId = item.SubdivisionId
                        };

                        Subdivisions.Add(subdivision);
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
