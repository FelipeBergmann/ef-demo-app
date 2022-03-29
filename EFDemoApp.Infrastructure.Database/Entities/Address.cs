namespace EFDemoApp.Infrastructure.Database.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Country Country { get; set; }
        public string Zipcode { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
