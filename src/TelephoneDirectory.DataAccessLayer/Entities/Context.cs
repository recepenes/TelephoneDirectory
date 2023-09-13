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

    }
}
