using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.UserBooks
{
    public class UserBookAppService :
     CrudAppService<
         UserBook,
         UserBookDto, 
         Guid, 
         PagedAndSortedResultRequestDto,
         CreateUpdateUserBooktDto>, 
     IUserBookAppService 
    {
        public UserBookAppService(IRepository<UserBook, Guid> repository)
            : base(repository)
        {

        }
    }
}
