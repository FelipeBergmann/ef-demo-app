using EFDemoApp.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDemoApp.Infrastructure.Database.Mappings
{
    internal class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.Birthday).IsRequired();

            builder.Property<bool>("IsDeleted").IsRequired().HasDefaultValue(false);
            builder.Property<DateTime?>("DeletedAt").IsRequired(false);

            builder.HasQueryFilter(person => !EF.Property<bool>(person, "IsDeleted"));

            builder.HasMany<Address>(p => p.Addresses)
                   .WithOne(a => a.Person)
                   .HasForeignKey(a => a.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Email>(p => p.EmailAddresses)
                    .WithOne(e => e.Person)
                    .HasForeignKey(e => e.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
