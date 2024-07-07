

using Domain.AggregateRoots;

namespace Infrastructure.Data.Models
{
    public class GetFilteredOrdersModel
    {
        public List<Order> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
