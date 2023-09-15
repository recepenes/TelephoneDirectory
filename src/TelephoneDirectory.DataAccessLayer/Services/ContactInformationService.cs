using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Services;

public class ContactInformationService : IContactInformationService
{
    private readonly Context _context;

    public ContactInformationService(Context context)
    {
        _context = context;
    }

    public async Task Create(CreateContactInformation model)
    {
        var isContentExist = await _context.Contacts.AnyAsync(x => x.Id == model.ContactId);
        ContactInformation contact = new()
        {
            ContactId = model.ContactId,
            ContactInformationType = model.ContactInformationType,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
        };
        _context.ContactInformation.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await _context
            .ContactInformation
            .SingleOrDefaultAsync(x => x.Id == id);

        entity.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}