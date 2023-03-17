using System;
using System.Threading.Tasks;
using BAXMobile.Model;

namespace BAXMobile.Service
{
    public interface IMobileSummaryDataManager
    {
        event EventHandler DataUpdated;
        string ErrorMessage { get; }
        bool IsLoading { get; }
        SummarisedLedgerMobileData SummaryData { get; }
        Task<bool> GetData();
    }
}