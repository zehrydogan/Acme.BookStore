using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.BookComments
{
    public class BookCommentAppService :
     CrudAppService<
         BookComment,
         BookCommentDto, 
         Guid, 
         PagedAndSortedResultRequestDto,
         CreateUpdateBookCommentDto>,
     IIBookCommentAppService
    {
        public BookCommentAppService(IRepository<BookComment, Guid> repository)
            : base(repository)
        {

        }
    }
}
