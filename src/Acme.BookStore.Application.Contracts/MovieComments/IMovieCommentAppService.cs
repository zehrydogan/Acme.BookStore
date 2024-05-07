using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.MovieComments
{
    public interface IMovieCommentAppService : ICrudAppService< 
        MovieCommentDto, 
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateMovieCommentDto> 
    {
    }
}
