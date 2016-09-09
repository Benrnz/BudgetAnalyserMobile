using System.Threading.Tasks;
using BAXMobile.Model;

namespace BAXMobile.Buckets
{
    public class LedgerBucketViewModel : SummarisedLedgerBucket
    {
        public LedgerBucketViewModel(SummarisedLedgerBucket bucketData)
        {
            BucketCode = bucketData.BucketCode;
            Description = bucketData.Description;
            BucketType = bucketData.BucketType;
            MonthlyBudgetAmount = bucketData.MonthlyBudgetAmount;
            OpeningBalance = bucketData.OpeningBalance;
            RemainingBalance = bucketData.RemainingBalance;
        }
        
        public async Task PageIsLoading()
        {
            await Task.Delay(500);
        }
    }
}