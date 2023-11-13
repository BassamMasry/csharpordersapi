using Microsoft.AspNetCore.Mvc;
using CSharpOrders.Models;
using CSharpOrders.Repositories;

namespace CSharpOrders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProviderController : ControllerBase
{
    private readonly ILogger<ProviderController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProviderRepository _providerRepository;

    public ProviderController(ILogger<ProviderController> logger,
                           IOrdersRepository ordersRepository,
                           IOrderItemsRepository orderItemsRepository,
                           IProviderRepository providerRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _orderItemsRepository = orderItemsRepository;
        _providerRepository = providerRepository;
    }

    [HttpGet(Name = "GetProvider")]
    public IEnumerable<Provider> Get()
    {
        if (HttpContext.Request.Query.Count == 0)
        {
            return _providerRepository.GetAll();
        } else
        {
            return _providerRepository.GetWithParameters(HttpContext.Request.Query);
        }
    }

    [HttpGet("{id}", Name = "GetProviderById")]
    public Provider? GetById(int id)
    {
        return _providerRepository.Get(id);
    }

    [HttpPost(Name = "PostProvider")]
    [Consumes("multipart/form-data")]
    public ActionResult<Provider> Create([FromForm]Provider provider)
    {
        if (ModelState.IsValid)
        {
            provider.Id = 0;
            _providerRepository.Create(provider);
            return CreatedAtAction(nameof(GetById), new { id = provider.Id }, provider);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpPut("{id}", Name = "PutProvider")]
    [Consumes("multipart/form-data")]
    public ActionResult<Provider> Update(int id, [FromForm]Provider provider)
    {
        if (ModelState.IsValid)
        {
            provider.Id = id;
            _providerRepository.Update(provider);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{id}", Name = "DeleteProvider")]
    [Consumes("multipart/form-data")]
    public ActionResult<Provider> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            _providerRepository.Delete(id);
            return NoContent();
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

}
