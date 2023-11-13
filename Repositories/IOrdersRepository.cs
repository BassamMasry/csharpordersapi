using CSharpOrders.Models;

namespace CSharpOrders.Repositories;

public interface IOrdersRepository
{
    Order? Create(Order order);
    void Delete(int id);
    Order? Update(Order order);
    Order? Get(int id);
    IEnumerable<Order> GetWithParameters(IQueryCollection parameters);
    IEnumerable<Order> GetAll();
}