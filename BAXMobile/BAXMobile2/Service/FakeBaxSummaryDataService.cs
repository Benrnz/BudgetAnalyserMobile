using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BAXMobile.Model;

namespace BAXMobile.Service
{
    public class FakeBaxSummaryDataService : IBaxSummaryDataService
    {
        public async Task<SummarisedLedgerMobileData> DownloadDataAsync()
        {
            await Task.Delay(3000);
            var data = new SummarisedLedgerMobileData
            {
                Exported = new DateTime(2016, 9, 2, 21, 48, 33),
                LastTransactionImport = new DateTime(2016, 8, 27, 20, 43, 11),
                StartOfMonth = new DateTime(2016, 8, 20),
                Title = "Smith Budget 2016"
            };

            data.LedgerBuckets.AddRange(new List<SummarisedLedgerBucket>
            {
                new SummarisedLedgerBucket
                {
                    BucketCode = "CLOTHES",
                    BucketType = "Accumulated Expense",
                    Description = "Clothing related bucket",
                    MonthlyBudgetAmount = 250,
                    OpeningBalance = 554.12M,
                    RemainingBalance = 498.98M
                },
                new SummarisedLedgerBucket
                {
                    BucketCode = "FUEL",
                    BucketType = "Spent Monthly Expense",
                    Description = "Petrol, gas etc",
                    MonthlyBudgetAmount = 110,
                    OpeningBalance = 110M,
                    RemainingBalance = 34.55M
                },
                new SummarisedLedgerBucket
                {
                    BucketCode = "FOOD",
                    BucketType = "Spent Monthly Expense",
                    Description = "Groceries, fruit and vege, and non-eating out",
                    MonthlyBudgetAmount = 900,
                    OpeningBalance = 900M,
                    RemainingBalance = 442.56M
                },
                new SummarisedLedgerBucket
                {
                    BucketCode = "SURPLUS",
                    BucketType = "Calculated Surplus",
                    Description = "Discretionary and unclassified incidental spending",
                    MonthlyBudgetAmount = 1000,
                    OpeningBalance = 1000M,
                    RemainingBalance = 743.15M
                }
            });

            return data;
        }
    }
}