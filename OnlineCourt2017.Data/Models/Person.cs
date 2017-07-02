using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }
    }
}
