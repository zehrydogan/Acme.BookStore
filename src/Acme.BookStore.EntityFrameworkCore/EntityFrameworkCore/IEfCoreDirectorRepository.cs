using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.BookStore.Directors
{
    public interface IEfCoreDirectorRepository
    {
        Task<Director> FindByNameAsync(string name);
        Task<List<Director>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}