using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BAXMobile.Model;
using BAXMobile.Service;
using JetBrains.Annotations;

namespace BAXMobile.Overview
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        private readonly IMobileSummaryDataManager dataManager;
        private string errorMessage;
        private bool isLoading;
        private SummarisedLedgerMobileData model;
        private string staleWarning;

        public OverviewViewModel([NotNull] IMobileSummaryDataManager dataManager)
        {
            if (dataManager == null) throw new ArgumentNullException(nameof(dataManager));
            this.dataManager = dataManager;
            this.dataManager.DataUpdated += OnDataUpdated;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorMessage
        {
            get { return this.errorMessage; }
            private set
            {
                if (value == this.errorMessage) return;
                this.errorMessage = value;
                OnPropertyChanged();
            }
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
            IsLoading = true;
            StaleWarning = string.Empty;
            await this.dataManager.GetData();
            if (this.dataManager.IsLoading) return;
            UpdateWithNewData();
            IsLoading = false;
        }

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
            IsLoading = false;
            Model = this.dataManager.SummaryData;
            ErrorMessage = this.dataManager.ErrorMessage;
            if (Model != null)
            {
                if (DateTime.Now.Subtract(Model.LastTransactionImport).TotalDays >= 5)
                {
                    StaleWarning = "WARNING: New transactions have not been imported for 5 or more days.";
                }
            }
        }
    }
}