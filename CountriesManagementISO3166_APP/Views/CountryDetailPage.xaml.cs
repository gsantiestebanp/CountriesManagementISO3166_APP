using CountriesManagementISO3166_APP.Dtos;
using Xamarin.Forms;

namespace CountriesManagementISO3166_APP.Views
{
    public partial class CountryDetailPage : ContentPage
    {
        public CountryDetailPage()
        {
            InitializeComponent();
        }

        private void SwipeItemView_Invoked(object sender, System.EventArgs e)
        {
            var subdivision = ((SwipeItemView)sender).BindingContext as SubdivisionDTO;

            MessagingCenter.Send(subdivision, "Edit");
        }
    }
}
