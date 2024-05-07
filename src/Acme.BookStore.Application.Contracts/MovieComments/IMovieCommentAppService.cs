using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.MovieComments
{
    public interface IMovieCommentAppService :IApplicationService
    {
        Task<MovieCommentDto> GetAsync(Guid MovieId);
        Task<MovieCommentDto> CreateAsync(CreateUpdateMovieCommentDto input);

        Task UpdateAsync(Guid MovieId, CreateUpdateMovieCommentDto input);

        Task DeleteAsync(Guid MovieId);
    }
}
