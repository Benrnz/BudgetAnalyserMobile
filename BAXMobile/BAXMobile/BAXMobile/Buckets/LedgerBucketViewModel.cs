using System;
using BAXMobile.Model;

namespace BAXMobile.Buckets
{
    public class LedgerBucketViewModel : SummarisedLedgerBucket
    {
        private double progress;

        public LedgerBucketViewModel(SummarisedLedgerBucket bucketData)
        {
            BucketCode = bucketData.BucketCode;
            Description = bucketData.Description;
            BucketType = bucketData.BucketType;
            MonthlyBudgetAmount = bucketData.MonthlyBudgetAmount;
            OpeningBalance = bucketData.OpeningBalance;
            RemainingBalance = bucketData.RemainingBalance;
            if (OpeningBalance == 0)
            {
                Progress = 0;
            }
            else
            {
                Progress = (double)RemainingBalance / (double)OpeningBalance;
            }
        }
        
        public double Progress
        {
            get { return this.progress; }
            set
            {
                if (Math.Abs(value - this.progress) < 0.001) return;
                this.progress = value;
                OnPropertyChanged();
            }
        }
    }
}