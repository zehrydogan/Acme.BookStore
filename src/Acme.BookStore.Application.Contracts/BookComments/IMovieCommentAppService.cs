using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BookComments
{
    public interface IMovieCommentAppService : ICrudAppService< 
        BookCommentDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateBookCommentDto> 
    {
    }
}
