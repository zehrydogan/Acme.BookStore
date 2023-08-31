using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Directors;

public interface IDirectorAppService : IApplicationService
{
    Task<DirectorDto> GetAsync(Guid id);

    Task<PagedResultDto<DirectorDto>> GetListAsync(GetDirectorListDto input);

    Task<DirectorDto> CreateAsync(CreateUpdateDirectorDto input);

    Task UpdateAsync(Guid id, CreateUpdateDirectorDto input);

    Task DeleteAsync(Guid id);
}
