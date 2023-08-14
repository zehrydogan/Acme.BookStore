using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.BookStore.Authors
{
    public interface IEfCoreAuthorRepository
    {
        Task<Author> FindByNameAsync(string name);
        Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}