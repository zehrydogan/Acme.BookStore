using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Actors;

public class ActorManager : DomainService
{
    private readonly IActorRepository _actorRepository;

    public ActorManager(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<Actor> CreateAsync(
        [NotNull] string name,
        [NotNull] string gender,

        DateTime birthDate)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingActor = await _actorRepository.FindByNameAsync(name);
        if (existingActor != null)
        {
            throw new ActorAlreadyExistsException(name);
        }

        return new Actor(
            GuidGenerator.Create(),
            name,
            gender,
            birthDate
            
        );
    }

    public async Task ChangeNameAsync(
        [NotNull] Actor actor,
        [NotNull] string newName)
    {
        Check.NotNull(actor, nameof(actor));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingActor = await _actorRepository.FindByNameAsync(newName);
        if (existingActor != null && existingActor.Id != actor.Id)
        {
            throw new ActorAlreadyExistsException(newName);
        }

        actor.ChangeName(newName);
    }
}
