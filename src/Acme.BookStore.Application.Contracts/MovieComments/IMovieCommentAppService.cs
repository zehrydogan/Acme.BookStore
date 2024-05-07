using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.MovieComments
{
    public interface IMovieCommentAppService : ICrudAppService< //Defines CRUD methods
        MovieCommentDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMovieCommentDto> //Used to create/update a book
    {
    }
}
