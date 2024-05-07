using Acme.BookStore.Books;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.BookComments
{
    public class BookCommentAppService :
     CrudAppService<
         BookComment, //The Book entity
         BookCommentDto, //Used to show books
         Guid, //Primary key of the book entity
         CreateUpdateBookCommentDto>, //Used to create/update a book
     IBookCommentAppService //implement the IBookAppService
    {
        public BookCommentAppService(IRepository<BookComment, Guid> repository)
            : base(repository)
        {

        }

        public Task<BookCommentDto> CreateAsync(CreateUpdateBookCommentDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid BookId, CreateUpdateBookCommentDto input)
        {
            throw new NotImplementedException();
        }
    }
}
