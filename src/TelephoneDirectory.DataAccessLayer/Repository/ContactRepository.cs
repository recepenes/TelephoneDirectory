using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;

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
            return await context.Contacts.Where(x => x.Id == id).Include(x => x.ContactInformation).SingleAsync();
        }

        public async Task Delete(Guid id)
        {
            var contact = await GetContactByGuid(id);
            contact.DeletedAt = DateTime.UtcNow;
            context.Contacts.Update(contact);
        }

        public async Task<GetContactDetail> GetContactDetail(Guid id)
        {
            var contact = context
           .Contacts
           .Include(x => x.ContactInformation)
           .Where(x => x.Id == id)
           .SingleOrDefault();

            return new GetContactDetail(contact.Name, contact.Surname, (IList<GetContactInformation>?)contact.ContactInformation, contact.CreatedAt);
        }

        public Task<GetReportContent[]> GetReportData()
        {
            return context.ContactInformation
                .Where(x => x.ContactInformationType == ContactInformationTypeEnum.Location)
                .GroupBy(x => x.Content)
                .Select(x => new GetReportContent
                (
                    x.Key,
                    x.Count(),
                    context
                        .ContactInformation
                        // Count of all phone numbers for each location
                        .Count(y => y.ContactInformationType == ContactInformationTypeEnum.PhoneNumber &&
                                    x.Select(c => c.ContactId).Contains(y.ContactId))
                ))
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
