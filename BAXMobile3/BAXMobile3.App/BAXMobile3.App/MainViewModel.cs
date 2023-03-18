using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BAXMobile.Buckets;
using BAXMobile.Overview;
using BAXMobile.Service;
using JetBrains.Annotations;

namespace BAXMobile
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IMobileSummaryDataManager dataManager;
        private string title;

        public MainViewModel([NotNull] OverviewViewModel overview, [NotNull] BucketsListViewModel bucketsViewModel, [NotNull] IMobileSummaryDataManager dataManager)
        {
            if (overview == null) throw new ArgumentNullException(nameof(overview));
            if (bucketsViewModel == null) throw new ArgumentNullException(nameof(bucketsViewModel));
            if (dataManager == null) throw new ArgumentNullException(nameof(dataManager));
            this.dataManager = dataManager;
            this.dataManager.DataUpdated += OnDataUpdated;
            OverviewViewModel = overview;
            BucketsListViewModel = bucketsViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BucketsListViewModel BucketsListViewModel { get; }

        public OverviewViewModel OverviewViewModel { get; }

        public string Title
        {
            get => this.title;
            private set
            {
                if (value == this.title) return;
                this.title = value;
                OnPropertyChanged();
            }
        }

        public async Task PageIsLoading()
        {
            await this.dataManager.GetData();
            UpdateWithNewData();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnDataUpdated(object sender, EventArgs eventArgs)
        {
            UpdateWithNewData();
        }

        private void UpdateWithNewData()
        {
            Title = this.dataManager.SummaryData?.Title;
        }
    }
}