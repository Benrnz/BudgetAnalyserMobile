using System;
using System.ComponentModel;
using System.Reflection;
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
        private static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public OverviewViewModel([NotNull] IMobileSummaryDataManager dataManager)
        {
            this.dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager));
            this.dataManager.DataUpdated += OnDataUpdated;
            this.ErrorMessage = Version;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorMessage
        {
            get => this.errorMessage;
            private set
            {
                if (value == this.errorMessage) return;
                this.errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => this.isLoading;
            private set
            {
                if (value == this.isLoading) return;
                this.isLoading = value;
                OnPropertyChanged();
            }
        }

        public SummarisedLedgerMobileData Model
        {
            get => this.model;
            private set
            {
                this.model = value;
                OnPropertyChanged();
            }
        }

        public string StaleWarning
        {
            get => this.staleWarning;
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
            if (this.dataManager.ErrorMessage == null)
                ErrorMessage = Version;
            else
                ErrorMessage = Version + this.dataManager.ErrorMessage;

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