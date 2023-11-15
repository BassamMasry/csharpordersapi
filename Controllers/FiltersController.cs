using Microsoft.AspNetCore.Mvc;
using CSharpOrders.Models;
using CSharpOrders.Repositories;
using System.Collections;

namespace CSharpOrders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiltersController : ControllerBase
{
    private readonly ILogger<FiltersController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProviderRepository _providerRepository;

    public FiltersController(ILogger<FiltersController> logger,
                           IOrdersRepository ordersRepository,
                           IOrderItemsRepository orderItemsRepository,
                           IProviderRepository providerRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _orderItemsRepository = orderItemsRepository;
        _providerRepository = providerRepository;
    }

    [HttpGet("orders", Name = "GetOrderFilters")]
    public ArrayList GetOrderFilters()
    {
        return _ordersRepository.GetFilters();
    }

    [HttpGet("orderItems", Name = "GetOrderItemsFilters")]
    public ArrayList GetOrderItemsFilters()
    {
        return _orderItemsRepository.GetFilters();
    }

    [HttpGet("providers", Name = "GetProviderFilters")]
    public ArrayList GetProviderFilters()
    {
        return _providerRepository.GetFilters();
    }

}
