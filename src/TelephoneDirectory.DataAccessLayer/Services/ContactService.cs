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
            return await repo.GetContactDetail(id);
        }

        public async Task<GetReportContent[]> GetReportData()
        {
            return await repo.GetReportData();
        }
    }
}
