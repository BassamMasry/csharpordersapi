using CSharpOrders.Data;
using CSharpOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpOrders.Repositories;

public class EFProviderRepository : IProviderRepository
{
    private readonly ProjectContext dbContext;

    public EFProviderRepository(ProjectContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Provider? Create(Provider provider)
    {
        dbContext.Providers.Add(provider);
        dbContext.SaveChanges();
        return provider;
    }

    public void Delete(int id)
    {
        dbContext.Providers.Where(provider => provider.Id == id).ExecuteDelete();
    }

    public Provider? Get(int id)
    {
        return dbContext.Providers.Find(id);
    }

    public IEnumerable<Provider> GetAll()
    {
        return dbContext.Providers.AsNoTracking().ToList();
    }

    public Provider? Update(Provider provider)
    {
        dbContext.Providers.Update(provider);
        dbContext.SaveChanges();
        return provider;
    }
}