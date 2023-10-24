using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.DataAccessLayer.Entities

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public Context()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Contact>? Contacts { get; set; }
        public virtual DbSet<ContactInformation>? ContactInformation { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

    }
}
