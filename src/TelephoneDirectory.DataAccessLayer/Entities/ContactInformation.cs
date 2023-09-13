using System.ComponentModel.DataAnnotations.Schema;

namespace TelephoneDirectory.DataAccessLayer.Entities;

public class ContactInformation : BaseEntity
{
    public Guid ContactId { get; set; }
    [ForeignKey("ContactId")] public Contact Contact { get; set; }

    public ContactInformationTypeEnum ContactInformationType { get; set; }
    public string Content { get; set; }
}