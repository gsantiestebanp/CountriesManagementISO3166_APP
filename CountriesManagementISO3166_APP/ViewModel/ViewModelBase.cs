using Acr.UserDialogs;
using CountriesManagementISO3166_APP.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;

namespace CountriesManagementISO3166_APP.ViewModel
{
    public class ViewModelBase : BindableBase, IInitialize, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs UserDialogs { get; }

        public static bool IsBusy { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, IUserDialogs userDialogs)
        {
            NavigationService = navigationService;
            UserDialogs = userDialogs;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }     

        public virtual void Destroy()
        {

        }
    }
}
