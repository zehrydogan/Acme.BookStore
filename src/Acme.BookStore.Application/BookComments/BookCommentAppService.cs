using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.BookComments
{
    public class BookCommentAppService :
     CrudAppService<
         BookComment, //The Book entity
         BookCommentDto, //Used to show books
         Guid, //Primary key of the book entity
         PagedAndSortedResultRequestDto,
         CreateUpdateBookCommentDto>, //Used to create/update a book
     IBookCommentAppService //implement the IBookAppService
    {
        public BookCommentAppService(IRepository<BookComment, Guid> repository)
            : base(repository)
        {

        }
    }
}
