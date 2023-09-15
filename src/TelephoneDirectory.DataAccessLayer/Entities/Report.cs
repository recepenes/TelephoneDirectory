using TelephoneDirectory.DataAccessLayer.Enums;

namespace TelephoneDirectory.DataAccessLayer.Entities
{
    public class Report : BaseEntity
    {
        public string? FilePath { get; set; }

        public ReportStatusEnum ReportStatus { get; set; }
    }
}
