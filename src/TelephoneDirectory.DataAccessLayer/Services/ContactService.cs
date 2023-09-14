using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;
using TelephoneDirectory.DataAccessLayer.Repository;

namespace TelephoneDirectory.DataAccessLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository repo;

        public ContactService(IContactRepository repo)
        {
            this.repo = repo;
        }

        public async Task Create(CreateContact model)
        {
            Contact contact = new Contact()
            {
                Name = model.Name,
                Surname = model.Surname,
                Company = model.Company,
                CreatedAt = DateTime.UtcNow
            };
            await repo.Add(contact);
        }

        public async Task Delete(Guid id)
        {
            var contact = await repo.GetContactByGuid(id);
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
            await repo.Delete(id);

        }

        public async Task<List<GetContact>> GetAll()
        {
            List<GetContact> getContacts = new();
            var listdto = await repo.GetAll();

            if (listdto == null)
            {
                throw new NullReferenceException("Kayıt Bulunumadı");
            }

            foreach (var item in listdto)
            {
                if (item.CreatedAt != null)
                {
                    GetContact newModel =
                        new(item.Id, item.Name, item.Surname, (DateTime)item.CreatedAt);
                    getContacts.Add(newModel);
                }
            }

            return getContacts;
        }

        public async Task<GetContactDetail> GetContactDetail(Guid id)
        {
            var contact = await repo.GetContactByGuid(id);

            GetContactDetail getContacts = new(contact.Name, contact.Surname, contact.ContactInformation
                , contact.CreatedAt);

            return getContacts;
        }

        public async Task CreateContactInformation(Guid id, List<CreateContactInformation> contactInformation)
        {
            var contact = await repo.GetContactByGuid(id);
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            var list = new List<GetContactInformation>();

            foreach (var item in contactInformation)
            {
                var contacts = new GetContactInformation(item.ContactId, item.ContactInformationType,
                     item.Content, DateTime.UtcNow);
                list.Add(contacts);

            }
            await repo.CreateContactInformation(id, list);
        }

        public async Task DeleteContactInformation(Guid id)
        {
            var contact = await repo.GetContactByGuid(id);
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
            await repo.DeleteContactInformation(id);
        }
    }
}
