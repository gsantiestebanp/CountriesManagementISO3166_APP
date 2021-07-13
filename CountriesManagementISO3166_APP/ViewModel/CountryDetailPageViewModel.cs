using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Dtos;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace CountriesManagementISO3166_APP.ViewModel
{
    public class CountryDetailPageViewModel : ViewModelBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;
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
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
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
