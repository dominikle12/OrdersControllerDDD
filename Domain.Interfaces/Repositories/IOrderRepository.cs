
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<GetFilteredOrdersModel> GetFilteredOrders(Pagination paginationDto, string username, string userRoles);
    }
}
