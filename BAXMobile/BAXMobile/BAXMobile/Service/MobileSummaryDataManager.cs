using System;
using System.Threading.Tasks;
using BAXMobile.Model;
using JetBrains.Annotations;

namespace BAXMobile.Service
{
    public class MobileSummaryDataManager : IMobileSummaryDataManager
    {
        private readonly IBaxSummaryDataService dataService;
        private DateTime lastUpdate;

        public MobileSummaryDataManager([NotNull] IBaxSummaryDataService dataService)
        {
            if (dataService == null) throw new ArgumentNullException(nameof(dataService));
            this.dataService = dataService;
        }

        public event EventHandler DataUpdated;

        public bool IsLoading { get; private set; }

        public SummarisedLedgerMobileData SummaryData { get; private set; }

        public async Task<bool> GetData()
        {
            if (DateTime.Now.Subtract(this.lastUpdate).TotalMinutes <= 3) return false;
            IsLoading = true;
            this.lastUpdate = DateTime.Now;
            SummaryData = await this.dataService.DownloadDataAsync();
            DataUpdated?.Invoke(this, EventArgs.Empty);
            IsLoading = false;
            return true;
        }
    }
}