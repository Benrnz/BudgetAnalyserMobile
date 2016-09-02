﻿using System;
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
        private SummarisedLedgerMobileData model;
        private bool isLoading;
        private string staleWarning;

        public OverviewViewModel([NotNull] IMobileSummaryDataManager dataManager)
        {
            if (dataManager == null) throw new ArgumentNullException(nameof(dataManager));
            this.dataManager = dataManager;
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
            IsLoading = true;
            this.staleWarning = string.Empty;
            await this.dataManager.GetData();
            Model = this.dataManager.SummaryData;
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