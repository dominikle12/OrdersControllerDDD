using Domain.SharedKernel;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        public Customer()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
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
