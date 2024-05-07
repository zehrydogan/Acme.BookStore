using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BookComments
{
    public interface IBookCommentAppService : IApplicationService
    {

        Task<BookCommentDto> GetAsync(Guid BookId);

        Task<BookCommentDto> CreateAsync(CreateUpdateBookCommentDto input);

        Task UpdateAsync(Guid BookId, CreateUpdateBookCommentDto input);

        Task DeleteAsync(Guid BookId);
    }
}
