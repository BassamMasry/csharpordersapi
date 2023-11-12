using System.Reflection;
using CSharpOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpOrders.Data;

public class ProjectContext : DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Provider> Providers => Set<Provider>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}