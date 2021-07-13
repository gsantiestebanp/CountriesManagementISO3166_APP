using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Entities.Request;
using CountriesManagementISO3166_APP.Infraestructure;
using CountriesManagementISO3166_APP.Interfaces;
using CountriesManagementISO3166_APP.Services;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;

namespace CountriesManagementISO3166_APP.ViewModel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class LoginPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IUserDialogs _userDialogs;
        public readonly IUserService _userService;

        public DelegateCommand LoginCmd { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IUserService userService, 
            IUserDialogs userDialogs) 
            : base(navigationService, userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _userService = userService;            

            LoginCmd = new DelegateCommand(LoginExecute);
        }

        private async void LoginExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            Email = "test@domain.com";
            Password = "abc123";
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    if (CheckInternetAccess.CheckConnection())
                    {
                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                        {
                            AuthenticateME authenticationData = new AuthenticateME()
                            {
                                Email = Email,
                                Password = Password
                            };
                            var responseMessage = await _userService.Login(authenticationData);

                            GlobalProperties data = GlobalProperties.Get();

                            data.UserId = responseMessage.Id;
                            data.Email = responseMessage.Email;
                            data.Token = responseMessage.Token;
                            data.Add();

                            IsBusy = false;
                            await _navigationService.NavigateAsync("CountryListPage");
                        }
                        else
                        {
                            IsBusy = false;
                            await _userDialogs.AlertAsync("Complete los campos");
                        }
                    }
                    else
                    {
                        IsBusy = false;
                        await _userDialogs.AlertAsync("No tiene acceso a internet", "No hay internet");
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
