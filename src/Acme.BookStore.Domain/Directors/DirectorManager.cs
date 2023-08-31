using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Directors;

public class DirectorManager : DomainService
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorManager(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    public async Task<Director> CreateAsync(
        [NotNull] string name,

        DateTime birthDate,
        [NotNull] GenderType gender
)


    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingDirector = await _directorRepository.FindByNameAsync(name);
        if (existingDirector != null)
        {
            throw new DirectorAlreadyExistsException(name);
        }

        return new Director(
            GuidGenerator.Create(),
            name,
            birthDate,
            gender

        );
    }

    public async Task ChangeNameAsync(
        [NotNull] Director director,
        [NotNull] string newName)
    {
        Check.NotNull(director, nameof(director));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingDirector = await _directorRepository.FindByNameAsync(newName);
        if (existingDirector != null && existingDirector.Id != director.Id)
        {
            throw new DirectorAlreadyExistsException(newName);
        }

        director.ChangeName(newName);
    }
}
