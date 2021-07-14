using Android.App;
using Android.OS;

namespace CountriesManagementISO3166_APP.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", Icon = "@mipmap/icon", NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}