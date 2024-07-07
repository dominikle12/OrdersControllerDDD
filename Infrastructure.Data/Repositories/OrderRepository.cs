using Domain.AggregateRoots;
using Domain.Entities;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<OrderContext> _dbOptions;

        public async Task<GetFilteredOrdersModel> GetFilteredOrders(Pagination paginationDto, string username, string userRoles)
        {
            using var _dbContext = new OrderContext(_dbOptions);

            var department = userRoles.Contains("ITS_MODE_ESM") ? "ESM" : "ESD";

            #region SortIfColumnIsAssignee
            if (paginationDto.SortField == "Assignee")
            {
                if (!String.IsNullOrEmpty(department))
                {
                    if (paginationDto.SortField == "ESD")
                    {
                        paginationDto.SortField = "EsdAgent";
                    }
                    else
                    {
                        paginationDto.SortField = "EsmAgent";
                    }
                }
            }
            #endregion

            if (String.IsNullOrEmpty(paginationDto.SortField))
            {
                //Ako se ovo dogodi, znači da je na frontendu pritisnut CLEAR ALL FILTERS
                paginationDto.SortField = "Woid";
                paginationDto.SortOrder = -1;
            }

            #region Filters
            var dbFilters = new List<Filter>();

            var filter = paginationDto.Filters["Id"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "Id",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["CreationDate"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "CreationDate",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["CustomerId"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "CustomerId",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }


            filter = paginationDto.Filters["AdapterId"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "AdapterId",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            string departmentAssigneeAttribute = "EsmAgent";
            if (department == "ESD")
            {
                departmentAssigneeAttribute = "EsdAgent";
            }

            filter = paginationDto.Filters["Assignee"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = departmentAssigneeAttribute,
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["LocationId"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "LocationId",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["CrmTariffName"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "CrmTariffName",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["CrmRootName"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "CrmRootName",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["WorkorderType"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "WorkorderType",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["Status"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "Status",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["StatusChangedAt"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "StatusChangedAt",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["StatusChangedAt"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "StatusChangedAt",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["ConstructionStatus"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "ConstructionStatus",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            filter = paginationDto.Filters["LastUpdatedBy"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "LastUpdatedBy",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }
            #endregion

            filter = paginationDto.Filters["Technology"].FirstOrDefault();
            if (filter != null)
            {
                dbFilters.Add(new Filter()
                {
                    Attribute = "Endpoint.Technology",
                    MatchMode = filter.MatchMode,
                    Value = filter.Value
                });
            }

            var query = _dbContext.Orders.AsQueryable();

            if (string.IsNullOrEmpty(paginationDto.SortField) == false)
            {
                if (paginationDto.SortField == "Technology")
                {
                    if (paginationDto.SortOrder == 1)
                        query = query.OrderBy(p => p.Technology);
                    else
                        query = query.OrderByDescending(p => p.Technology);
                }
                else if (paginationDto.SortField == "WorkorderType")
                {
                    if (paginationDto.SortOrder == 1)
                        query = query.OrderBy(p => p.OrderType);
                    else
                        query = query.OrderByDescending(p => p.OrderType);
                }
                else
                {
                    if (typeof(Order).GetProperty(paginationDto.SortField) == null)
                        throw new Exception($"Property {paginationDto.SortField} could not be linked in the query.");
                    else
                    {
                        if (paginationDto.SortOrder == 1)
                            query = query.OrderBy(p => EF.Property<Order>(p, paginationDto.SortField));
                        else
                            query = query.OrderByDescending(p => EF.Property<Order>(p, paginationDto.SortField));
                    }
                }
            }

           

            query = query.Where(x => x.Status == "New" || x.Status == "In progress" || x.Status == "On hold" || x.Status == "Billing done" || x.Status == "Billing canceled");

            
            var count = query.Count();
            query = query
                .Skip(paginationDto.First)
                .Take(paginationDto.Rows);

            var str = query.ToQueryString();

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
                AssignedToDepartment = department,
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
                TotalRecords = count
            };

            return retVal;
        }
        public async Task<Customer> AddCustomer(AddCustomerRequest request)
        {
            using var _dbContext = new OrderContext(_dbOptions);

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
    }
}
