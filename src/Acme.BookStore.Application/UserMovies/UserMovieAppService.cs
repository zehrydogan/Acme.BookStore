using System;
using System.Threading.Tasks;
using Volo.Abp;
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

        public override async Task<UserMovieDto> CreateAsync(CreateUpdateUserMovietDto input)
        {
            var isExist = await base.Repository.AnyAsync(c => c.UserId == input.UserId && c.MovieId == input.MovieId);
            if (isExist) throw new UserFriendlyException("Bu Film Daha Önceden Listeye Eklenmiş!");

            var result = await base.CreateAsync(input);
            return result;
        }
    }
}
