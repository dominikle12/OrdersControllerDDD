using Domain.AggregateRoots;
using System.Collections.Generic;

namespace Infrastructure.Data.Models.Responses
{
    public class GetFilteredOrdersResponse : BaseResponse
    {
        public List<Order> Data { get; set; } = new List<Order>();
        public int TotalRecords { get; set; }
    }
}
