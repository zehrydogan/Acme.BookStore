using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.UserMovies
{
    public class UserMovieAppService :
     CrudAppService<
         UserMovie,
         UserMovieDto,
         Guid,
         PagedAndSortedResultRequestDto,
         CreateUpdateUserMovietDto>,
     IUserMovieAppService
    {
        public UserMovieAppService(IRepository<UserMovie, Guid> repository)
            : base(repository)
        {

        }
    }
}
