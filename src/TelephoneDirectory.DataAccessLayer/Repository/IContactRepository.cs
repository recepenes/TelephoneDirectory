using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Repository
{
    public interface IContactRepository
    {
        Task Add(Contact contact);
        Task Delete(Guid id);
        Task<List<Contact>> GetAll();
        Task<Contact> GetContactByGuid(Guid id);
        Task<GetContactDetail> GetContactDetail(Guid id);
        Task<GetReportContent[]> GetReportData();
    }
}