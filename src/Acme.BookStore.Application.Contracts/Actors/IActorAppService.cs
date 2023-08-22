using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Actors;

public interface IActorAppService : IApplicationService
{
    Task<ActorDto> GetAsync(Guid id);

    Task<PagedResultDto<ActorDto>> GetListAsync(GetActorListDto input);

    Task<ActorDto> CreateAsync(CreateUpdateActorDto input);

    Task UpdateAsync(Guid id, CreateUpdateActorDto input);

    Task DeleteAsync(Guid id);
}
