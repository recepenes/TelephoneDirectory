using System.ComponentModel.DataAnnotations;

namespace TelephoneDirectory.DataAccessLayer.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
