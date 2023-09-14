using TelephoneDirectory.DataAccessLayer.Entities;

namespace TelephoneDirectory.DataAccessLayer.Repository
{
    public interface IContactRepository
    {
        Task Add(Contact contact);
        Task Delete(Guid id);
        Task<List<Contact>> GetAll();
        Task<Contact> GetContactByGuid(Guid id);
    }
}