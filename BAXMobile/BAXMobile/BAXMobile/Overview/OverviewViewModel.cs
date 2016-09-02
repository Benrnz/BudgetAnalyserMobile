using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BAXMobile.Model;
using BAXMobile.Service;

namespace BAXMobile.Overview
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        private readonly IBaxSummaryDataService dataService;
        private SummarisedLedgerMobileData model;
        private DateTime lastUpdate;
        private bool isLoading;
        private string staleWarning;

        public OverviewViewModel(IBaxSummaryDataService dataService)
        {
            if (dataService == null) throw new ArgumentNullException(nameof(dataService));
            this.dataService = dataService;
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

        public SummarisedLedgerMobileData Model
        {
            get { return this.model; }
            private set
            {
                this.model = value;
                OnPropertyChanged();
            }
        }

        public string StaleWarning
        {
            get { return this.staleWarning; }
            private set
            {
                if (value == this.staleWarning) return;
                this.staleWarning = value;
                OnPropertyChanged();
            }
        }

        public async Task PageIsLoading()
        {
            if (DateTime.Now.Subtract(this.lastUpdate).TotalMinutes <= 3) return;
            IsLoading = true;
            this.staleWarning = string.Empty;
            this.lastUpdate = DateTime.Now;
            Model = await this.dataService.DownloadDataAsync();
            if (DateTime.Now.Subtract(Model.LastTransactionImport).TotalDays >= 5)
            {
                StaleWarning = "WARNING: New transactions have not been imported for 5 or more days.";
            }
            IsLoading = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}