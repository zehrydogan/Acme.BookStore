using System;
using System.Threading.Tasks;
using Volo.Abp;
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

        public override async Task<UserBookDto> CreateAsync(CreateUpdateUserBooktDto input)
        {
            var isExist = await base.Repository.AnyAsync(c => c.UserId == input.UserId && c.BookId == input.BookId);
            if (isExist) throw new UserFriendlyException("Bu Kitap Daha Önceden Listeye Eklenmiş!");

            var result = await base.CreateAsync(input);
            return result;
        }
    }
}