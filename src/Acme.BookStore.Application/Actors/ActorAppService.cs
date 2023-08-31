using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Actors;

[Authorize(BookStorePermissions.Actors.Default)]
public class ActorAppService : BookStoreAppService, IActorAppService
{
    private readonly IActorRepository _actorRepository;
    private readonly ActorManager _actorManager;

    public ActorAppService(
        IActorRepository actorRepository,
        ActorManager actorManager)
    {
        _actorRepository = actorRepository;
        _actorManager = actorManager;
    }
    public async Task<ActorDto> GetAsync(Guid id)
    {
        var actor = await _actorRepository.GetAsync(id);
        return ObjectMapper.Map<Actor, ActorDto>(actor);
    }
    public async Task<PagedResultDto<ActorDto>> GetListAsync(GetActorListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Actor.Name);
        }

        var actors = await _actorRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _actorRepository.CountAsync()
            : await _actorRepository.CountAsync(
                actor => actor.Name.Contains(input.Filter));

        return new PagedResultDto<ActorDto>(
            totalCount,
            ObjectMapper.Map<List<Actor>, List<ActorDto>>(actors)
        );
    }
    [Authorize(BookStorePermissions.Actors.Create)]
    public async Task<ActorDto> CreateAsync(CreateUpdateActorDto input)
    {
        var actor = await _actorManager.CreateAsync(
            input.Name,
            input.BirthDate,
            input.Gender
        );

        await _actorRepository.InsertAsync(actor);

        return ObjectMapper.Map<Actor, ActorDto>(actor);
    }
    [Authorize(BookStorePermissions.Actors.Edit)]
    public async Task UpdateAsync(Guid id, CreateUpdateActorDto input)
    {
        var actor = await _actorRepository.GetAsync(id);

        if (actor.Name != input.Name)
        {
            await _actorManager.ChangeNameAsync(actor, input.Name);
        }

        actor.Gender = input.Gender;

        actor.BirthDate = input.BirthDate;

        await _actorRepository.UpdateAsync(actor);
    }

    [Authorize(BookStorePermissions.Actors.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _actorRepository.DeleteAsync(id);
    }


    //...SERVICE METHODS WILL COME HERE...
}
