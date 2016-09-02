using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BAXMobile.Model
{
    /// <summary>
    ///     A DTO type to export information about one ledger bucket
    /// </summary>
    public class SummarisedLedgerBucket : INotifyPropertyChanged
    {
        private string bucketCode;
        private string bucketType;
        private string description;
        private decimal monthlyBudgetAmount;
        private decimal openingBalance;
        private decimal remainingBalance;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     The bucket code for the ledger
        /// </summary>
        public string BucketCode
        {
            get { return this.bucketCode; }
            set
            {
                if (this.bucketCode != value)
                {
                    this.bucketCode = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     The type (behaviour) of the bucket. IE: Spent Monthly or Saved.
        /// </summary>
        public string BucketType
        {
            get { return this.bucketType; }
            set
            {
                if (this.bucketType != value)
                {
                    this.bucketType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     The bucket description from the budget
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     The monthly budget amount credited into this ledger
        /// </summary>
        public decimal MonthlyBudgetAmount
        {
            get { return this.monthlyBudgetAmount; }
            set
            {
                if (this.monthlyBudgetAmount != value)
                {
                    this.monthlyBudgetAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     The opening balance at the begining of the month
        /// </summary>
        public decimal OpeningBalance
        {
            get { return this.openingBalance; }
            set
            {
                if (this.openingBalance != value)
                {
                    this.openingBalance = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     The funds remaining in the bucket
        /// </summary>
        public decimal RemainingBalance
        {
            get { return this.remainingBalance; }
            set
            {
                if (this.remainingBalance != value)
                {
                    this.remainingBalance = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}