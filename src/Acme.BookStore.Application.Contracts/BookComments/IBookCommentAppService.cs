using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BookComments
{
    public interface IBookCommentAppService : ICrudAppService< //Defines CRUD methods
        BookCommentDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookCommentDto> //Used to create/update a book
    {
    }
}
