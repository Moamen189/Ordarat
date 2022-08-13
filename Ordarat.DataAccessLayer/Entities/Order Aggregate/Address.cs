using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {

        }

        //For Use Later
        public Address(string firstName, string lastName, string country, string city, string streeet)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Streeet = streeet;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Streeet { get; set; }
    }
}
