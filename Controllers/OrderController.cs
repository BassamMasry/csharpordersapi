using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using CSharpOrders.Models;
using System.Net;
using CSharpOrders.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CSharpOrders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrdersRepository _ordersRepository;

    public OrderController(ILogger<OrderController> logger, IOrdersRepository ordersRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
    }

    [HttpGet(Name = "GetOrder")]
    public IEnumerable<Order> Get()
    {
        return _ordersRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetOrderById")]
    public Order? GetById(int id)
    {
        return _ordersRepository.Get(id);
    }

    [HttpPost(Name = "PostOrder")]
    [Consumes("multipart/form-data")]
    public ActionResult<Order> Create([FromForm]Order order)
    {
        if (ModelState.IsValid)
        {
            order.Id = 0;
            _ordersRepository.Create(order);
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
    [Consumes("multipart/form-data")]
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
