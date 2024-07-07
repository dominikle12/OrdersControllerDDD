using AutoMapper;
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

        [HttpPost("get-filtered-orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFilteredOrdersResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<GetFilteredOrdersResponse>> GetFilteredOrders([FromQuery] string username, [FromQuery] string userRoles, [FromBody] GetFilteredRequest request)
        {
            _logger.LogInformation("GetFilteredOrders invoked with {@request}", request);

            try
            {
                #region Mapiranje
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GetFilteredRequest, Pagination>();
                });

                var mapper = new Mapper(config);

                var pagination = mapper.Map<Pagination>(request);
                #endregion

                var result = await _ordersService.GetFilteredOrders(pagination, username, userRoles);

                if (result == null)
                {
                    return this.Problem($"GetFilteredOrders returned null  for next request {request}", statusCode: 500);
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
    }
}
