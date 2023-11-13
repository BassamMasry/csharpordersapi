using CSharpOrders.Models;

namespace CSharpOrders.Repositories;

public interface IProviderRepository
{
    Provider? Create(Provider provider);
    void Delete(int id);
    Provider? Update(Provider provider);
    Provider? Get(int id);
    IEnumerable<Provider> GetWithParameters(IQueryCollection parameters);
    IEnumerable<Provider> GetAll();
}