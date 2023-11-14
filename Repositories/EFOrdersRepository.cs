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

    public IEnumerable<Order> GetWithParameters(IQueryCollection parameters)
    {
        string number = parameters["number"].ToString();
        string startDate = parameters["startDate"].ToString();
        string endDate = parameters["endDate"].ToString();
        string providerId = parameters["providerId"].ToString();

        IQueryable<Order> query = dbContext.Orders;

        if (!string.IsNullOrWhiteSpace(number))
        {
            query = query.Where(order => order.Number.Contains(number));
        }
        if (!string.IsNullOrWhiteSpace(startDate))
        {
            try
            {
                var queryDate = DateTime.Parse(startDate);
                query = query.Where(order => order.Date.CompareTo(queryDate) >= 0);
            }
            catch (System.FormatException){;}
        } else
        {
            query = query.Where(order => order.Date.CompareTo(DateTime.Now.AddDays(-30)) > 0);
        }
        if (!string.IsNullOrWhiteSpace(endDate))
        {
            try
            {
                var queryDate = DateTime.Parse(endDate);
                query = query.Where(order => order.Date.CompareTo(queryDate) <= 0);
            }
            catch (System.FormatException){;}
        }
        if (!string.IsNullOrWhiteSpace(providerId)){
            query = query.Where(order => order.ProviderId == int.Parse(providerId));
        }

        return query.ToList();
    }

    public Order? Update(Order order)
    {
        dbContext.Orders.Update(order);
        dbContext.SaveChanges();
        return order;
    }
}