namespace SimpleResults.Example.AspNetCore.Controllers.ManualValidation
{
    [Route("Order-ManualValidation")]
    [Tags("Order ManualValidation")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public Result<CreatedGuid> Create([FromBody]Order order)
        {
            if (ModelState.IsFailed())
                return ModelState.Invalid();

            return _orderService.Create(order);
        }
    }
}

namespace SimpleResults.Example.AspNetCore.Controllers.AutomaticValidation
{
    // Allows automatic model validation.
    [ApiController]
    [Route("Order-AutomaticValidation")]
    [Tags("Order AutomaticValidation")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public Result<CreatedGuid> Create([FromBody]Order order) 
            => _orderService.Create(order);
    }
}
