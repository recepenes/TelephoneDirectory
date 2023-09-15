using TelephoneDirectory.DataAccessLayer.Messages;
using TelephoneDirectory.ReportAPI.Records;

namespace TelephoneDirectory.ReportAPI.Services
{
    public interface IReportService
    {
        Task Complete(ReportMessage message);
        Task Generate();
        Task<GetReport[]> GetAll();
    }
}