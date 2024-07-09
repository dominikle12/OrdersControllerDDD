using Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models.Responses
{
    public class AddOrderResponse : BaseResponse
    {
        public AddOrderResponse(Order order, ResponseStatus status) : base()
        {
            this.Order = order;
            this.Status = status;
        }
        public Order Order { get; set; }
    }
}
