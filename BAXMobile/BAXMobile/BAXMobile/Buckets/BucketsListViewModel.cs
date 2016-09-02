using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BAXMobile.Model;
using BAXMobile.Service;
using JetBrains.Annotations;

namespace BAXMobile.Buckets
{
    public class BucketsListViewModel : INotifyPropertyChanged
    {
        private readonly IMobileSummaryDataManager dataManager;
        private bool isLoading;
        private SummarisedLedgerMobileData model;

        public BucketsListViewModel([NotNull] IMobileSummaryDataManager dataManager)
        {
            if (dataManager == null) throw new ArgumentNullException(nameof(dataManager));
            this.dataManager = dataManager;
        }

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

        public async Task PageIsLoading()
        {
            IsLoading = true;
            await this.dataManager.GetData();
            Model = this.dataManager.SummaryData;
            IsLoading = false;
        }

        public SummarisedLedgerMobileData Model
        {
            get { return this.model; }
            private set
            {
                this.model = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}