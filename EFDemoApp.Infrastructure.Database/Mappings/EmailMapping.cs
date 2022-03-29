using EFDemoApp.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDemoApp.Infrastructure.Database.Mappings
{
    internal class EmailMapping : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Emails");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Address).HasMaxLength(250).HasColumnType("nvarchar").IsRequired();

            builder.HasOne<Person>(a => a.Person)
                    .WithMany(p => p.EmailAddresses)
                    .HasForeignKey(e => e.PersonId);
        }
    }
}
