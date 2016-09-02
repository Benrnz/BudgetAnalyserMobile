using System.Threading.Tasks;
using BAXMobile.Model;

namespace BAXMobile.Service
{
    public interface IMobileSummaryDataManager
    {
        SummarisedLedgerMobileData SummaryData { get; }
        Task<bool> GetData();
    }
}