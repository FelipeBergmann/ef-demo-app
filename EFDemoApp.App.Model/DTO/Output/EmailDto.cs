using EFDemoApp.Infrastructure.Database.Entities;

public class EmailDto
{
    public Guid Id { get; set; }
    public string Address { get; set; }

    public static implicit operator EmailDto(Email email) => new() { Id = email.Id, Address = email.Address };
}