using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.MovieComments
{
    public class MovieCommentAppService :
       CrudAppService<
           MovieComment, 
           MovieCommentDto, 
           Guid, 
           PagedAndSortedResultRequestDto,
           CreateUpdateMovieCommentDto>, 
       IMovieCommentAppService 
    {
        public MovieCommentAppService(IRepository<MovieComment, Guid> repository)
            : base(repository)
        {

        }
    }
}