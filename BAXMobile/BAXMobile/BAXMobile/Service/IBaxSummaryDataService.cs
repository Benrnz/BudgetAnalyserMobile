using System.Threading.Tasks;
using BAXMobile.Model;

namespace BAXMobile.Service
{
    public interface IBaxSummaryDataService
    {
        Task<SummarisedLedgerMobileData> DownloadDataAsync();
    }
}