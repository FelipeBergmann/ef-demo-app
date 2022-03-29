using EFDemoApp.Infrastructure.Database.Entities;

public class AddressDto
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public Country Country { get; set; }
    public string Zipcode { get; set; }

    public static implicit operator AddressDto(Address address) => new() { Id = address.Id, Street = address.Street, Number = address.Number, Complement = address.Complement, City = address.City, State = address.State, Country = address.Country, Zipcode = address.Zipcode };
}