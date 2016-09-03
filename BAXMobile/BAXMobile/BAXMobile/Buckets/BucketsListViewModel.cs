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

        public SummarisedLedgerBucket SelectedBucket
        {
            get { return this.selectedBucket; }
            set
            {
                if (Equals(value, this.selectedBucket)) return;
                this.selectedBucket = value;
                OnPropertyChanged();
                if (this.selectedBucket != null) NavigateToBucket(SelectedBucket);
            }
        }

        private void NavigateToBucket(SummarisedLedgerBucket bucket)
        {
            Application.Current.MainPage.DisplayAlert(Application.Current.MainPage.Title, $"You selected {bucket.Description}", "OK");
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

        public async Task PageIsLoading()
        {
            IsLoading = true;
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