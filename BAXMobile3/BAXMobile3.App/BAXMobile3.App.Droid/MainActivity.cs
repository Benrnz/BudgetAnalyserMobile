using Android.App;
using Android.Content.PM;
using Android.OS;
using BAXMobile;

namespace BaxMobile3.Droid
{
    [Activity(Label = "BAX Mobile",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme", 
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            var app = new App();
            app.CompositeRoot(new AndroidHashingAlgorithm());
            LoadApplication(app);
        }
    }
}