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

        public OverviewViewModel(IBaxSummaryDataService dataService)
        {
            if (dataService == null) throw new ArgumentNullException(nameof(dataService));
            this.dataService = dataService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SummarisedLedgerMobileData Model
        {
            get { return this.model; }
            private set
            {
                this.model = value;
                OnPropertyChanged();
            }
        }

        public async Task PageIsLoading()
        {
            if (DateTime.Now.Subtract(this.lastUpdate).TotalMinutes <= 3) return;
            this.lastUpdate = DateTime.Now;
            Model = await this.dataService.DownloadDataAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}