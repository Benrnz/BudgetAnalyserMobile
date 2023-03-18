using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BAXMobile.Model
{
    /// <summary>
    ///     A DTO type to export all data about the state of the ledger at a point in time.
    /// </summary>
    public class SummarisedLedgerMobileData : INotifyPropertyChanged
    {
        private DateTime exported;
        private DateTime lastTransactionImport;
        private List<SummarisedLedgerBucket> ledgerBuckets;
        private DateTime startOfMonth;
        private string title;

        /// <summary>
        ///     Instantiate a new instance of <see cref="SummarisedLedgerMobileData" />
        /// </summary>
        public SummarisedLedgerMobileData()
        {
            LedgerBuckets = new List<SummarisedLedgerBucket>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     The date and time this object was exported from Budget Analyser
        /// </summary>
        public DateTime Exported
        {
            get { return this.exported; }
            set
            {
                if (value.Equals(this.exported)) return;
                this.exported = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The date and time transactions were last imported into BudgetAnalyser from the bank.
        /// </summary>
        public DateTime LastTransactionImport
        {
            get { return this.lastTransactionImport; }
            set
            {
                if (value.Equals(this.lastTransactionImport)) return;
                this.lastTransactionImport = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     All the ledger buckets in the ledger
        /// </summary>
        public List<SummarisedLedgerBucket> LedgerBuckets
        {
            get { return this.ledgerBuckets; }
            private set
            {
                if (Equals(value, this.ledgerBuckets)) return;
                this.ledgerBuckets = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The date this month started
        /// </summary>
        public DateTime StartOfMonth
        {
            get { return this.startOfMonth; }
            set
            {
                if (value.Equals(this.startOfMonth)) return;
                this.startOfMonth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The title of the budget
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set
            {
                if (value == this.title) return;
                this.title = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}