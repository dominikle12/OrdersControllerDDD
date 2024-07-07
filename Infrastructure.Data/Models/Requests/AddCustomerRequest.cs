using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models.Requests
{
    public class AddCustomerRequest
    {
        public string Name { get; set; }
        public string OIB { get; set; }
        public Address Address { get; set; }
        public string OibName { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerContactEmail { get; set; }
        public string CustomerContactPhone { get; set; }
        public string TechnicalContactName { get; set; }
        public string TechnicalContactEmail { get; set; }
        public string TechnicalContactPhone { get; set; }
        public string Segment { get; set; }
        public string KAM { get; set; }
        public string TAM { get; set; }
        public string CustomerCode { get; set; }
    }
}
