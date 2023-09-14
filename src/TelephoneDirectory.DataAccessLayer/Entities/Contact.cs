using System.ComponentModel.DataAnnotations;
using TelephoneDirectory.DataAccessLayer.Records;

namespace TelephoneDirectory.DataAccessLayer.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Company { get; set; }
        public IList<GetContactInformation> ContactInformation { get; set; }
    }
}
