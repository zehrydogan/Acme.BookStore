using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.UserBooks
{
    public interface IUserBookAppService : ICrudAppService< 
        UserBookDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateUserBooktDto> 
    {
    }
}
