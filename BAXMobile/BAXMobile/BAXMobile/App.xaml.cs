using System;
using System.Diagnostics;
using BAXMobile.Buckets;
using BAXMobile.Overview;
using BAXMobile.Service;
using Xamarin.Forms;

namespace BAXMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainViewModel = CompositeRoot();
            MainPage = new MainPage {BindingContext = mainViewModel};
        }

        private MainViewModel CompositeRoot()
        {
            var dataManager = new MobileSummaryDataManager(new FakeBaxSummaryDataService());

            return new MainViewModel(
                new OverviewViewModel(dataManager),
                new BucketsListViewModel(dataManager)
            );
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void CurrentDomainUnhandledException(object sender, object ex, bool isTerminating)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
}