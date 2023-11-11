using CSharpOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpOrders.Data;

public class OrdersContext : DbContext
{
    public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders => Set<Order>();
}