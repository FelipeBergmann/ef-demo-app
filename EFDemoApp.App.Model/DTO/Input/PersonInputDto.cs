using EFDemoApp.Infrastructure.Database.Entities;

public class PersonInputDto 
{
    public PersonInputDto(string firstName, string lastName, DateTime birthday)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }

    public Guid Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public List<AddressInputDto> Addresses { get; set; } = new List<AddressInputDto>();
    public List<EmailInputDto> EmailAddresses { get; set; } = new List<EmailInputDto>();

    public static implicit operator Person(PersonInputDto person) => new()
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
        Birthday = person.Birthday,
        Addresses = person.Addresses.ConvertAll(dto => (Address)dto),
        EmailAddresses = person.EmailAddresses.ConvertAll(dto => (Email)dto)
    };
}
