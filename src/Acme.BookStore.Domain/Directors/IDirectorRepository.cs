using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Directors;

public interface IDirectorRepository : IRepository<Director, Guid>
{
    Task<Director> FindByNameAsync(string name);

    Task<List<Director>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
