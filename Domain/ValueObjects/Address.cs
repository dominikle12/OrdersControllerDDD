using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Address
    {
        public Address()
        {
            
        }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string Phone { get; set; }

        public static Address CreateAddress(string city, string street, string postalCode, string houseNumber, string country, string phone)
        {
            return new Address(city, street, postalCode, houseNumber, country, phone);
        }

        private Address(string city, string street, string postalCode, string houseNumber, string country, string phone)
        {
            City = city;
            Street = street;
            PostalCode = postalCode;
            Country = country;
            HouseNumber = houseNumber;
            Phone = phone;
        }

    }
}
