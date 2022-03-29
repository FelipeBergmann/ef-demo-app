using EFDemoApp.Infrastructure.Database.Entities;

public class EmailInputDto 
{
    public EmailInputDto(string address)
    {
        Id = Guid.NewGuid();
        Address = address;
    }

    public Guid Id { get; private set; }
    public string Address { get; set; }

    public static implicit operator Email(EmailInputDto email) => new() { Id = email.Id, Address = email.Address };
}
