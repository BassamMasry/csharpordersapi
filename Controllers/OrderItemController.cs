using Microsoft.AspNetCore.Mvc;
using CSharpOrders.Models;
using CSharpOrders.Repositories;

namespace CSharpOrders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly ILogger<OrderItemController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProviderRepository _providerRepository;

    public OrderItemController(ILogger<OrderItemController> logger,
                           IOrdersRepository ordersRepository,
                           IOrderItemsRepository orderItemsRepository,
                           IProviderRepository providerRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _orderItemsRepository = orderItemsRepository;
        _providerRepository = providerRepository;
    }

    [HttpGet(Name = "GetOrderItem")]
    public IEnumerable<OrderItem> Get()
    {
        if (HttpContext.Request.Query.Count == 0)
        {
            return _orderItemsRepository.GetAll();
        } else
        {
            return _orderItemsRepository.GetWithParameters(HttpContext.Request.Query);
        }
        
    }

    [HttpGet("{id}", Name = "GetOrderItemById")]
    public OrderItem? GetById(int id)
    {
        return _orderItemsRepository.Get(id);
    }

    [HttpPost(Name = "PostOrderItem")]
    [Consumes("multipart/form-data")]
    public ActionResult<OrderItem> Create([FromForm]OrderItem orderItem)
    {
        if (ModelState.IsValid)
        {
            try
            {
                orderItem.Order = _ordersRepository.Get(orderItem.OrderId) ?? throw new ArgumentNullException(nameof(orderItem));
            }
            catch (ArgumentNullException)
            {
                ModelState.AddModelError("orderId", "That order does not exist");
                return BadRequest(ModelState);
            }
            if (orderItem.Name == orderItem.Order.Number)
            {
                ModelState.AddModelError("name", "The orderItem name cannot be equal to the order number");
                return BadRequest(ModelState);
            }
            orderItem.Id = 0;
            _orderItemsRepository.Create(orderItem);
            return CreatedAtAction(nameof(GetById), new { id = orderItem.Id }, orderItem);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut("{id}", Name = "PutOrderItem")]
    [Consumes("multipart/form-data")]
    public ActionResult<OrderItem> Update(int id, [FromForm]OrderItem orderItem)
    {
        if (ModelState.IsValid)
        {
            orderItem.Id = id;
            orderItem.OrderId = _orderItemsRepository.Get(id)?.OrderId ?? throw new ArgumentNullException(nameof(orderItem));
            _orderItemsRepository.Update(orderItem);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{id}", Name = "DeleteOrderItem")]
    public ActionResult<OrderItem> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            _orderItemsRepository.Delete(id);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

}
