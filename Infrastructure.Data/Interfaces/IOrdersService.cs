using Domain.AggregateRoots;
using Domain.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Infrastructure.Data.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces
{
    public interface IOrdersService
    {
        Task<GetFilteredOrdersResponse> GetOrders();
        Task<Customer> AddCustomer(AddCustomerRequest request);
        Task<AddOrderResponse> AddOrder(AddOrderRequest request);
        Task<List<Order>> GetAllCustomerOrders(Guid customerId);
    }
}
