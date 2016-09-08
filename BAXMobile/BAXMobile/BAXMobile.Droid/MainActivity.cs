using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace BAXMobile.Droid
{
    [Activity(Label = "BAX Mobile", 
        Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            var app = new App();
            app.CompositeRoot(new AndroidHashingAlgorithm());
            LoadApplication(app);
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            App.CurrentDomainUnhandledException(sender, e.ExceptionObject, e.IsTerminating);
        }
    }
}