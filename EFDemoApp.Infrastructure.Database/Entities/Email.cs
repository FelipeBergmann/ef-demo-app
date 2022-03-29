using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemoApp.Infrastructure.Database.Entities
{
    public class Email
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
