using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BookComments
{
    public interface IBookCommentAppService : ICrudAppService< 
        BookCommentDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateBookCommentDto> 
    {
    }
}
