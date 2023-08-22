using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Actors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Movies;

[Authorize(BookStorePermissions.Movies.Default)]
public class MovieAppService :
    CrudAppService<
        Movie, //The Book entity
        MovieDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMovieDto>, //Used to create/update a book
        IMovieAppService //implement the IBookAppService
{
    private readonly IActorRepository _actorRepository;

    public MovieAppService(
        IRepository<Movie, Guid> repository,
        IActorRepository actorRepository)
        : base(repository)
    {
        _actorRepository = actorRepository;
        GetPolicyName = BookStorePermissions.Movies.Default;
        GetListPolicyName = BookStorePermissions.Movies.Default;
        CreatePolicyName = BookStorePermissions.Movies.Create;
        UpdatePolicyName = BookStorePermissions.Movies.Edit;
        DeletePolicyName = BookStorePermissions.Movies.Delete;
    }





    public override async Task<MovieDto> GetAsync(Guid id)
    {
        var queryable = await Repository.GetQueryableAsync();

        var query = from movie in queryable
                    join actor in await _actorRepository.GetQueryableAsync() on movie.ActorId equals actor.Id
                    where movie.Id == id
                    select new { movie, actor };

        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Movie), id);
        }

        var movieDto = ObjectMapper.Map<Movie, MovieDto>(queryResult.movie);
        movieDto.ActorName = queryResult.actor.Name;
        return movieDto;
    }

    public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await Repository.GetQueryableAsync();

        var query = from movie in queryable 
                    join actor in await _actorRepository.GetQueryableAsync() on movie.ActorId equals actor.Id
                    select new { movie, actor };

        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(query);

        var movieDtos = queryResult.Select(x =>
        {
            var movieDto = ObjectMapper.Map<Movie, MovieDto>(x.movie);
            movieDto.ActorName = x.actor.Name;
            return movieDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<MovieDto>(
            totalCount,
            movieDtos
        );
    }

    public async Task<ListResultDto<ActorLookupDto>> GetActorLookupAsync()
    {
        var actors = await _actorRepository.GetListAsync();

        return new ListResultDto<ActorLookupDto>(
            ObjectMapper.Map<List<Actor>, List<ActorLookupDto>>(actors)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"movie.{nameof(Movie.Name)}";
        }

        if (sorting.Contains("actorName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "actorName",
                "actor.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"movie.{sorting}";
    }
}
