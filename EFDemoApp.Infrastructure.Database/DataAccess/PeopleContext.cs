using EFDemoApp.Infrastructure.Database.Entities;
using EFDemoApp.Infrastructure.Database.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EFDemoApp.Infrastructure.Database.DataAccess
{
    public class PeopleContext : DbContext

    {
        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
            modelBuilder.ApplyConfiguration(new EmailMapping());
        }
    }
}
