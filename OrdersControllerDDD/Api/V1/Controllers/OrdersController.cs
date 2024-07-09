using AutoMapper;
using Domain.AggregateRoots;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Requests;
using Infrastructure.Data.Models.Responses;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;


namespace OrdersControllerDDD.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orders")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _ordersService;
        public OrdersController(
            ILogger<OrdersController> logger,
            IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        [HttpPost("get-orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFilteredOrdersResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<GetFilteredOrdersResponse>> GetOrders()
        {
            _logger.LogInformation("GetFilteredOrders invoked with ");

            try
            {

                var result = await _ordersService.GetOrders();

                if (result == null)
                {
                    return this.Problem("GetFilteredOrders returned null", statusCode: 500);
                }

                var retVal = new GetFilteredOrdersResponse()
                {
                    Data = result.Data,
                    TotalRecords = result.TotalRecords,
                    Status = ResponseStatus.Success
                };

                return this.Ok(retVal);

            }
            catch (Exception ex)
            {
                return this.BadRequest(new BaseResponse()
                {
                    Status = ResponseStatus.UnhandledException,
                    MessageTranslationCode = TranslationCode.UnknownError,
                    LogMessage = ex.Message
                });
            }
        }
        [HttpPost("add-customer")]
        public async Task<ActionResult> AddCustomer([FromBody]AddCustomerRequest request)
        {
            var result = await _ordersService.AddCustomer(request);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] AddOrderRequest request)
        {
            var result = await _ordersService.AddOrder(request);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
        [HttpGet("get-all-customer-orders")]
        public async Task<ActionResult<List<Order>>> GetAllCustomerOrders([FromQuery] Guid customerId)
        {
            var result = await _ordersService.GetAllCustomerOrders(customerId);
            return Ok(result);
        }
    }
}
