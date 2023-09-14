using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.DataAccessLayer.Entities;

namespace TelephoneDirectory.DataAccessLayer.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context context;

        public ContactRepository(Context context)
        {
            this.context = context;
        }

        public async Task Add(Contact contact)
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAll()
        {
            return await context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactByGuid(Guid id)
        {
            return await context.Contacts.Where(x => x.Id == id).SingleAsync();
        }

        public async Task Delete(Guid id)
        {
            var contact = await GetContactByGuid(id);
            contact.DeletedAt = DateTime.UtcNow;
            context.Contacts.Update(contact);
        }
    }
}
