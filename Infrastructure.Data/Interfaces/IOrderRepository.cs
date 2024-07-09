using Domain.AggregateRoots;
using Domain.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Infrastructure.Data.Models.Responses;

namespace Infrastructure.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<GetFilteredOrdersModel> GetOrders();
        Task<Customer> AddCustomer(AddCustomerRequest request);
        Task<AddOrderResponse> AddOrder(AddOrderRequest request);
        Task<List<Order>> GetAllCustomerOrders(Guid customerId);
    }
}
