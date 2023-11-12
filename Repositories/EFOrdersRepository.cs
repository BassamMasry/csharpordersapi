using CSharpOrders.Data;
using CSharpOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpOrders.Repositories;

public class EFOrdersRepository : IOrdersRepository
{
    private readonly ProjectContext dbContext;

    public EFOrdersRepository(ProjectContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Order? Create(Order order)
    {
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
        return order;
    }

    public void Delete(int id)
    {
        dbContext.Orders.Where(order => order.Id == id).ExecuteDelete();
    }

    public Order? Get(int id)
    {
        return dbContext.Orders.Find(id);
    }

    public IEnumerable<Order> GetAll()
    {
        return dbContext.Orders.AsNoTracking().ToList();
    }

    public Order? Update(Order order)
    {
        dbContext.Orders.Update(order);
        dbContext.SaveChanges();
        return order;
    }
}