using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Repository
{
    public interface IContactRepository
    {
        Task Add(Contact contact);
        Task CreateContactInformation(Guid id, IList<GetContactInformation> contactInformation);
        Task Delete(Guid id);
        Task DeleteContactInformation(Guid id);
        Task<List<Contact>> GetAll();
        Task<Contact> GetContactByGuid(Guid id);
    }
}