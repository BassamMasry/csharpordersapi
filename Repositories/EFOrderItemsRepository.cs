using System.Collections;
using CSharpOrders.Data;
using CSharpOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpOrders.Repositories;

public class EFOrderItemsRepository : IOrderItemsRepository
{
    private readonly ProjectContext dbContext;

    public EFOrderItemsRepository(ProjectContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public OrderItem? Create(OrderItem orderItem)
    {
        dbContext.OrderItems.Add(orderItem);
        dbContext.SaveChanges();
        return orderItem;
    }

    public void Delete(int id)
    {
        dbContext.OrderItems.Where(orderItem => orderItem.Id == id).ExecuteDelete();
    }

    public OrderItem? Get(int id)
    {
        return dbContext.OrderItems.Find(id);
    }

    public IEnumerable<OrderItem> GetAll()
    {
        return dbContext.OrderItems.AsNoTracking().ToList();
    }

    public ArrayList GetFilters()
    {
        return new ArrayList
        {
            dbContext.OrderItems.Select(orderItem => orderItem.Name).Distinct().ToList(),
            dbContext.OrderItems.Select(orderItem => orderItem.Unit).Distinct().ToList()
        };
    }

    public IEnumerable<OrderItem> GetWithParameters(IQueryCollection parameters)
    {
        string name = parameters["name"].ToString();
        string unit = parameters["unit"].ToString();
        string orderId = parameters["orderId"].ToString();

        IQueryable<OrderItem> query = dbContext.OrderItems;

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(orderItem => orderItem.Name.Contains(name));
        }
        if (!string.IsNullOrWhiteSpace(unit))
        {
            query = query.Where(orderItem => orderItem.Unit.Contains(unit));
        }
        if (!string.IsNullOrWhiteSpace(orderId))
        {
            query = query.Where(orderItem => orderItem.OrderId == int.Parse(orderId));
        }

        return query.ToList();
    }

    public OrderItem? Update(OrderItem orderItem)
    {
        dbContext.OrderItems.Update(orderItem);
        dbContext.SaveChanges();
        return orderItem;
    }
}