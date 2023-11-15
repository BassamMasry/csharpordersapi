using System.Collections;
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

    public ArrayList GetFilters()
    {
        return new ArrayList
        {
            dbContext.Orders.Where(order => order.Date.CompareTo(DateTime.Now.AddDays(-30)) > 0).Select(order => order.Number).Distinct().ToList(),
            dbContext.Orders.Where(order => order.Date.CompareTo(DateTime.Now.AddDays(-30)) > 0).Select(order => order.ProviderId).Distinct().ToList()
        };
    }

    public IEnumerable<Order> GetWithParameters(IQueryCollection parameters)
    {
        var number = parameters["number"].AsEnumerable();
        var startDate = parameters["startDate"].ToString();
        var endDate = parameters["endDate"].ToString();
        var providerId = parameters["providerId"].AsEnumerable();

        IQueryable<Order> query = dbContext.Orders;

        if (number is not null && number.Any())
        {
            query = query.Where(order => number.Contains(order.Number));
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
        if (providerId is not null && providerId.Any()){
            query = query.Where(order => providerId.Contains(order.ProviderId.ToString()));
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