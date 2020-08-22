using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BAXMobile.Model;
using BAXMobile.Service;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace BAXMobile.Buckets
{
    public class BucketsListViewModel : INotifyPropertyChanged
    {
        private readonly IMobileSummaryDataManager dataManager;
        private bool isLoading;
        private ObservableCollection<SummarisedLedgerBucket> ledgerBuckets;
        private INavigation navigator;
        private SummarisedLedgerBucket selectedBucket;

        public BucketsListViewModel([NotNull] IMobileSummaryDataManager dataManager)
        {
            if (dataManager == null) throw new ArgumentNullException(nameof(dataManager));
            this.dataManager = dataManager;
            this.dataManager.DataUpdated += OnDataUpdated;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoading
        {
            get { return this.isLoading; }
            private set
            {
                if (value == this.isLoading) return;
                this.isLoading = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SummarisedLedgerBucket> LedgerBuckets
        {
            get { return this.ledgerBuckets; }
            private set
            {
                if (Equals(value, this.ledgerBuckets)) return;
                this.ledgerBuckets = value;
                OnPropertyChanged();
            }
        }

        public SummarisedLedgerBucket SelectedBucket
        {
            get { return this.selectedBucket; }
            set
            {
                if (Equals(value, this.selectedBucket)) return;
                this.selectedBucket = value;
                OnPropertyChanged();
            }
        }

        public async Task NavigateToBucket()
        {
            if (this.navigator == null) return;
            await this.navigator.PushAsync(new BucketViewPage { BindingContext = new LedgerBucketViewModel(SelectedBucket) });
        }

        public async Task PageIsLoading(INavigation viewNavigator)
        {
            IsLoading = true;
            this.navigator = viewNavigator;
            await this.dataManager.GetData();
            if (this.dataManager.IsLoading) return;
            UpdateWithNewData();
            IsLoading = false;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnDataUpdated(object sender, EventArgs e)
        {
            UpdateWithNewData();
        }

        private void UpdateWithNewData()
        {
            LedgerBuckets?.Clear();
            SelectedBucket = null;
            IsLoading = false;
            if (this.dataManager.SummaryData == null) return;
            LedgerBuckets = new ObservableCollection<SummarisedLedgerBucket>(this.dataManager.SummaryData.LedgerBuckets);
        }
    }
}