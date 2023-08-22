using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.BookStore.Actors
{
    public interface IEfCoreActorRepository
    {
        Task<Actor> FindByNameAsync(string name);
        Task<List<Actor>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}