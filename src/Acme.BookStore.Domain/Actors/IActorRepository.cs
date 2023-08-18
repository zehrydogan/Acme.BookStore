using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Actors;

public interface IActorRepository : IRepository<Actor, Guid>
{
    Task<Actor> FindByNameAsync(string name);

    Task<List<Actor>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
