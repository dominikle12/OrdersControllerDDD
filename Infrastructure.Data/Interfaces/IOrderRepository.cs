using Domain.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;

namespace Infrastructure.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<GetFilteredOrdersModel> GetFilteredOrders(Pagination paginationDto, string username, string userRoles);
        Task<Customer> AddCustomer(AddCustomerRequest request);
    }
}
