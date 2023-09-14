using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Services
{
    public interface IContactService
    {
        public Task<List<GetContact>> GetAll();
        public Task Create(CreateContact model);
        public Task Delete(Guid id);
    }
}
