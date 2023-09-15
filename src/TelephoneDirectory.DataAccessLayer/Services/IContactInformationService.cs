using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Services
{
    public interface IContactInformationService
    {
        Task Create(CreateContactInformation model);
        Task Delete(Guid id);
    }
}