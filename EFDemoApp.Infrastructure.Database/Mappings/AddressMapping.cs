using EFDemoApp.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDemoApp.Infrastructure.Database.Mappings
{
    internal class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Street).HasMaxLength(150).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.Number).HasMaxLength(10).HasColumnType("varchar").IsRequired();
            builder.Property(x => x.Complement).HasMaxLength(100).HasColumnType("nvarchar");
            builder.Property(x => x.City).HasMaxLength(150).HasColumnType("nvarchar");
            builder.Property(x => x.State).HasMaxLength(150).HasColumnType("nvarchar");

            builder.HasOne<Person>(a => a.Person)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(a => a.PersonId);
        }
    }
}
