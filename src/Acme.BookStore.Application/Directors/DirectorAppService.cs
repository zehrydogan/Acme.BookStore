using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Directors;

[Authorize(BookStorePermissions.Directors.Default)]
public class DirectorAppService : BookStoreAppService, IDirectorAppService
{
    private readonly IDirectorRepository _directorRepository;
    private readonly DirectorManager _directorManager;

    public DirectorAppService(
        IDirectorRepository directorRepository,
        DirectorManager directorManager)
    {
        _directorRepository = directorRepository;
        _directorManager = directorManager;
    }
    public async Task<DirectorDto> GetAsync(Guid id)
    {
        var director = await _directorRepository.GetAsync(id);
        return ObjectMapper.Map<Director, DirectorDto>(director);
    }
    public async Task<PagedResultDto<DirectorDto>> GetListAsync(GetDirectorListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Director.Name);
        }

        var directors = await _directorRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _directorRepository.CountAsync()
            : await _directorRepository.CountAsync(
                director => director.Name.Contains(input.Filter));

        return new PagedResultDto<DirectorDto>(
            totalCount,
            ObjectMapper.Map<List<Director>, List<DirectorDto>>(directors)
        );
    }
    [Authorize(BookStorePermissions.Directors.Create)]
    public async Task<DirectorDto> CreateAsync(CreateUpdateDirectorDto input)
    {
        var director = await _directorManager.CreateAsync(
            input.Name,
            input.BirthDate,
            input.Gender
        );

        await _directorRepository.InsertAsync(director);

        return ObjectMapper.Map<Director, DirectorDto>(director);
    }
    [Authorize(BookStorePermissions.Directors.Edit)]
    public async Task UpdateAsync(Guid id, CreateUpdateDirectorDto input)
    {
        var director = await _directorRepository.GetAsync(id);

        if (director.Name != input.Name)
        {
            await _directorManager.ChangeNameAsync(director, input.Name);
        }

        director.Gender = input.Gender;

        director.BirthDate = input.BirthDate;

        await _directorRepository.UpdateAsync(director);
    }

    [Authorize(BookStorePermissions.Directors.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _directorRepository.DeleteAsync(id);
    }


    //...SERVICE METHODS WILL COME HERE...
}
