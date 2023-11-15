using Microsoft.AspNetCore.Mvc;
using CSharpOrders.Models;
using CSharpOrders.Repositories;

namespace CSharpOrders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProviderRepository _providerRepository;

    public OrderController(ILogger<OrderController> logger,
                           IOrdersRepository ordersRepository,
                           IOrderItemsRepository orderItemsRepository,
                           IProviderRepository providerRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _orderItemsRepository = orderItemsRepository;
        _providerRepository = providerRepository;
    }

    [HttpGet(Name = "GetOrder")]
    public IEnumerable<Order> Get()
    {
        return _ordersRepository.GetWithParameters(HttpContext.Request.Query);
    }

    [HttpGet("{id}", Name = "GetOrderById")]
    public Order? GetById(int id)
    {
        var order = _ordersRepository.Get(id);
        if (order is null)
        {
            return null;
        }
        order.Provider = _providerRepository.Get(order.ProviderId);
        return order;
    }

    [HttpPost(Name = "PostOrder")]
    [Consumes("multipart/form-data")]
    public ActionResult<Order> Create([FromForm]Order order)
    {
        if (ModelState.IsValid)
        {
            try
            {
                order.Provider = _providerRepository.Get(order.ProviderId) ?? throw new ArgumentNullException(nameof(order));
            }
            catch (ArgumentNullException)
            {
                ModelState.AddModelError("providerId", "That provider does not exist");
                return BadRequest(ModelState);
            }
            order.Id = 0;
            try
            {
                _ordersRepository.Create(order);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut("{id}", Name = "PutOrder")]
    [Consumes("multipart/form-data")]
    public ActionResult<Order> Update(int id, [FromForm]Order order)
    {
        if (ModelState.IsValid)
        {
            try
            {
                order.Provider = _providerRepository.Get(order.ProviderId) ?? throw new ArgumentNullException(nameof(order));
            }
            catch (ArgumentNullException)
            {
                ModelState.AddModelError("providerId", "That provider does not exist");
                return BadRequest(ModelState);
            }
            order.Id = id;
            _ordersRepository.Update(order);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    public ActionResult<Order> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            _ordersRepository.Delete(id);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

}
