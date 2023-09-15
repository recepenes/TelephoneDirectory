using EasyNetQ;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Enums;
using TelephoneDirectory.DataAccessLayer.Messages;
using TelephoneDirectory.ReportAPI.Records;

namespace TelephoneDirectory.ReportAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly IBus _bus;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ReportService(Context context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task Generate()
        {
            var report = new Report
            {
                ReportStatus = ReportStatusEnum.Processing
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            await _bus.PubSub.PublishAsync(new ReportMessage(report.Id, ""));
        }

        public Task<GetReport[]> GetAll()
        {
            return _context
                .Reports
                .ProjectToType<GetReport>(_mapper.Config)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task Complete(ReportMessage message)
        {
            var report = _context
                .Reports
                .SingleOrDefault(x => x.Id == message.Id);

            if (report != null && !string.IsNullOrEmpty(message.FilePath))
            {
                report.FilePath = message.FilePath;
                report.ReportStatus = ReportStatusEnum.Completed;

                await _context.SaveChangesAsync();
            }
        }
    }
}
