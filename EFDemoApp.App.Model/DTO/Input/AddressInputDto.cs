using EFDemoApp.Infrastructure.Database.Entities;

public class AddressInputDto
{
    public AddressInputDto(string street, string number, string complement, string city, string state, Country country, string zipcode)
    {
        Id = Guid.NewGuid();
        Street = street;
        Number = number;
        Complement = complement;
        City = city;
        State = state;
        Country = country;
        Zipcode = zipcode;
    }
    public Guid Id { get; private set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public Country Country { get; set; }
    public string Zipcode { get; set; }

    public static explicit operator Address(AddressInputDto address) => new() { Id = address.Id, Street = address.Street, Number = address.Number, Complement = address.Complement, City = address.City, State = address.State, Country = address.Country, Zipcode = address.Zipcode };
}