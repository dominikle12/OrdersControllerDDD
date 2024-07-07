using AutoMapper;
using Domain.Entities;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Infrastructure.Data.Models.Responses;

namespace Infrastructure.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetFilteredOrdersResponse> GetFilteredOrders(Pagination filters, string username, string userRoles)
        {
            var filteredOrders = await _orderRepository.GetFilteredOrders(filters, username, userRoles);
            var filteredOrdersReturn = new GetFilteredOrdersResponse();
            if (filteredOrders != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GetFilteredOrdersModel, GetFilteredOrdersResponse>();
                });
                var mapper = new Mapper(config);

                filteredOrdersReturn = mapper.Map<GetFilteredOrdersResponse>(filteredOrders);
            }

            return filteredOrdersReturn;
        }
        public async Task<Customer> AddCustomer(AddCustomerRequest request)
        {
            var response = await _orderRepository.AddCustomer(request);
            return response;
        }
    }
}
