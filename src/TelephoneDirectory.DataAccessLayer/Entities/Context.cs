using Microsoft.EntityFrameworkCore;

namespace TelephoneDirectory.DataAccessLayer.Entities

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<ContactInformation>? ContactInformation { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}
