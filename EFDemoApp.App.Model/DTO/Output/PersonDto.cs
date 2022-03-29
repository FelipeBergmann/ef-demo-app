using EFDemoApp.Infrastructure.Database.Entities;

public class PersonDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    public List<EmailDto> EmailAddresses { get; set; } = new List<EmailDto>();

    public static implicit operator PersonDto(Person person) => new() { Id = person.Id, FirstName = person.FirstName, LastName = person.LastName, Birthday = person.Birthday, EmailAddresses = person.EmailAddresses.ConvertAll(emailAddress => (EmailDto)emailAddress), Addresses = person.Addresses.ConvertAll(address => (AddressDto)address) };
}