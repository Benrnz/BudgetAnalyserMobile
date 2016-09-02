using BAXMobile.Buckets;
using BAXMobile.Overview;

namespace BAXMobile
{
    public class MainViewModel
    {
        public MainViewModel(OverviewViewModel overview, BucketsListViewModel bucketsViewModel)
        {
            OverviewViewModel = overview;
            BucketsListViewModel = bucketsViewModel;
        }

        public OverviewViewModel OverviewViewModel { get; private set; }

        public BucketsListViewModel BucketsListViewModel { get; private set; }
    }
}
