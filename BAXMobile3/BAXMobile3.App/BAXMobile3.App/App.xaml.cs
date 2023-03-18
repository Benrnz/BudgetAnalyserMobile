using System.Diagnostics;
using BAXMobile.Buckets;
using BAXMobile.Overview;
using BAXMobile.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace BAXMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public void CompositeRoot(IHashingAlgorithm hashingAlgorithm)
        {
            // var dataService = new FakeBaxSummaryDataService();
            // SecretCreds is a non-source-controlled file to store the two AWS credential values.
            var dataService = new AmazonS3BaxSummaryDataService(hashingAlgorithm, SecretCreds.AwsAccessKeyId, SecretCreds.AwsSecret);
            var dataManager = new MobileSummaryDataManager(dataService);

            var mainViewModel = new MainViewModel(
                new OverviewViewModel(dataManager),
                new BucketsListViewModel(dataManager),
                dataManager
            );

            MainPage = new MainPage { BindingContext = mainViewModel };
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