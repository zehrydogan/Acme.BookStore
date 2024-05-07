using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.MovieComments
{
    public class MovieCommentAppService :
       CrudAppService<
           MovieComment, //The Book entity
           MovieCommentDto, //Used to show books
           Guid, //Primary key of the book entity
           PagedAndSortedResultRequestDto,
           CreateUpdateMovieCommentDto>, //Used to create/update a book
       IMovieCommentAppService //implement the IBookAppService
    {
        public MovieCommentAppService(IRepository<MovieComment, Guid> repository)
            : base(repository)
        {

        }
    }
}