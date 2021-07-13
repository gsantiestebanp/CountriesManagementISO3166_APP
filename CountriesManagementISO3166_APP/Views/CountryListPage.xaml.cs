using CountriesManagementISO3166_APP.Dtos;
using Xamarin.Forms;

namespace CountriesManagementISO3166_APP.Views
{
    public partial class CountryListPage : ContentPage
    {
        public CountryListPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send<object>(this, "Search");
        }

        private void SwipeItemView_Invoked(object sender, System.EventArgs e)
        {
            var country = ((SwipeItemView)sender).BindingContext as CountryDTO;

            MessagingCenter.Send(country, "Delete");
        }
    }
}
