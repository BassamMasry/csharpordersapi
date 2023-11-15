using System.Collections;
using CSharpOrders.Models;

namespace CSharpOrders.Repositories;

public interface IOrderItemsRepository
{
    OrderItem? Create(OrderItem orderItem);
    void Delete(int id);
    OrderItem? Update(OrderItem orderItem);
    OrderItem? Get(int id);
    IEnumerable<OrderItem> GetWithParameters(IQueryCollection parameters);
    IEnumerable<OrderItem> GetAll();
    ArrayList GetFilters();
}