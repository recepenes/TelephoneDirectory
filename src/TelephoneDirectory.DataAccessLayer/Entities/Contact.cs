using System.ComponentModel.DataAnnotations;

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
        public IList<ContactInformation> ContactInformation { get; set; }
    }
}
