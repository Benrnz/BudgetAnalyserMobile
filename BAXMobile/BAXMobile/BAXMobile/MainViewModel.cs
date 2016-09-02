using BAXMobile.Buckets;
using BAXMobile.Overview;

namespace BAXMobile
{
    public class MainViewModel
    {
        public MainViewModel(OverviewViewModel overview)
        {
            OverviewViewModel = overview;
        }

        public OverviewViewModel OverviewViewModel { get; private set; }

        public BucketsListViewModel BucketsListViewModel { get; private set; }
    }
}
