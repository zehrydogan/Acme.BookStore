using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.UserMovies
{
    public interface IUserMovieAppService : ICrudAppService<
        UserMovieDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateUserMovietDto>
    {
    }
}
