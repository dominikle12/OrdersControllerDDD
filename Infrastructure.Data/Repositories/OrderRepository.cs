using Domain.AggregateRoots;
using Domain.Entities;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Infrastructure.Data.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _dbContext;

        public OrderRepository(OrderContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetFilteredOrdersModel> GetOrders()
        {
            var query = _dbContext.Orders.AsQueryable();

            var data = query.Select(x => new Order
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                CustomerId = x.CustomerId,
                AdapterId = x.AdapterId,
                Assignee = x.Assignee,
                LocationName = x.LocationName,
                //x.LocationId is not foreign key for Location table.
                LocationId = x.LocationId,
                //x.EndpointId is foreign key for Endpoint table
                EndpointId = x.EndpointId,
                AssignedToDepartment = x.AssignedToDepartment,
                CrmRootName = x.CrmRootName,
                CrmTariffName = x.CrmTariffName,
                OrderClassification = x.OrderClassification,
                Status = x.Status,
                StatusChangedDate = x.StatusChangedDate,
                OrderType = x.OrderType,
                Priority = x.Priority ?? "",
                IsOnHold = x.IsOnHold,
                Technology = x.Technology,
                OnHoldReason = x.OnHoldReason,

            }).ToList().Cast<Order>().ToList();

            var retVal = new GetFilteredOrdersModel()
            {
                Data = data,
                TotalRecords = data.Count
            };

            return retVal;
        } 
    

        public async Task<Customer> AddCustomer(AddCustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                OIB = request.OIB,
                OibName = request.OibName,
                Address = new Domain.ValueObjects.Address
                {
                    City = request.Address.City,
                    Country = request.Address.Country,
                    HouseNumber = request.Address.HouseNumber,
                    Phone = request.Address.Phone,
                    PostalCode = request.Address.PostalCode,
                    Street = request.Address.Street,
                },
                CustomerContactName = request.CustomerContactName,
                CustomerContactEmail = request.CustomerContactEmail,
                CustomerContactPhone = request.CustomerContactPhone,
                TechnicalContactName = request.TechnicalContactName,
                TechnicalContactEmail = request.TechnicalContactEmail,
                TechnicalContactPhone = request.TechnicalContactPhone,
                Segment = request.Segment,
                KAM = request.KAM,
                TAM = request.TAM,
                CustomerCode = request.CustomerCode
            };
            await _dbContext.Customers.AddAsync(customer);

            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<AddOrderResponse> AddOrder(AddOrderRequest request)
        {
            var customers = _dbContext.Customers;
            bool customerExists = false;
            foreach(var customer in customers)
            {
                if(customer.Id == request.CustomerId)
                {
                    customerExists = true;
                    break;
                }
            }
            if (customerExists)
            {
                var order = new Order
                {
                    AdapterId = request.AdapterId,
                    AssignedToDepartment = request.AssignedToDepartment,
                    Assignee = request.Assignee,
                    CreationDate = request.CreationDate,
                    CrmOrderNumber = request.CrmOrderNumber,
                    CrmRootName = request.CrmRootName,
                    CrmTariffName = request.CrmTariffName,
                    CustomerId = request.CustomerId,
                    EndpointId = request.EndpointId,
                    IsOnHold = request.IsOnHold,
                    LocationId = request.LocationId,
                    LocationName = request.LocationName,
                    OnHoldReason = request.OnHoldReason,
                    OrderClassification = request.OrderClassification,
                    OrderType = request.OrderType,
                    Priority = request.Priority,
                    ServiceRequestId = request.ServiceRequestId,
                    Status = request.Status,
                    StatusChangedDate = request.StatusChangedDate,
                    Technology = request.Technology
                };
                await _dbContext.Orders.AddAsync(order);

                await _dbContext.SaveChangesAsync();

                return new AddOrderResponse(order, ResponseStatus.Success);
            }

            return new AddOrderResponse(new Order(), ResponseStatus.Error); 
            
        }

        public async Task<List<Order>> GetAllCustomerOrders(Guid customerId)
        {
            var data = await _dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

            return data;
        }
    }
}
