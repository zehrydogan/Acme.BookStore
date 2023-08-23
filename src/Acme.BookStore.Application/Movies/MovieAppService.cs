using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Actors;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Movies
{
    [Authorize(BookStorePermissions.Movies.Default)]
    public class MovieAppService : CrudAppService<MovieActor, MovieDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieDto>, IMovieAppService
    {
        private readonly IActorRepository _actorRepository;

        public MovieAppService(
            IRepository<MovieActor, Guid> repository,
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
            // Get the IQueryable<MovieActor> from the repository
            var queryable = await Repository.GetQueryableAsync();

            // Prepare a query to join movieactors, actors, and movies
            var query = from movieactor in queryable
                        join actor in await _actorRepository.GetQueryableAsync() on movieactor.ActorId equals actor.Id
                        join movie in await _actorRepository.GetQueryableAsync() on movieactor.MovieId equals movie.Id
                        where movieactor.Id == id
                        select new { movieactor, actor, movie };

            // Execute the query and get the movieactor with actor and movie
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(MovieActor), id);
            }

            // Map the query result to a MovieDto object
            var movieDto = ObjectMapper.Map<MovieActor, MovieDto>(queryResult.movieactor);

            // You can also set MovieId and ActorId
            movieDto.MovieId = queryResult.movie.Id;
            movieDto.ActorId = queryResult.actor.Id;

            return movieDto;
        }


        public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            // Get the IQueryable<MovieActor> from the repository
            var queryable = await Repository.GetQueryableAsync();

            // Prepare a query to join movieactors and actors
            var query = from movieactor in queryable
                        join actor in await _actorRepository.GetQueryableAsync() on movieactor.ActorId equals actor.Id
                        select new { movieactor, actor };

            // Paging
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            // Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            // Convert the query result to a list of MovieDto objects
            var movieDtos = queryResult.Select(x =>
            {
                var movieDto = ObjectMapper.Map<MovieActor, MovieDto>(x.movieactor);
                movieDto.ActorId = x.actor.Id; // Set ActorId here
                return movieDto;
            }).ToList();

            // Get the total count with another query
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
                return $"movieactor.{nameof(MovieActor.Id)}";
            }

            // You might need to adjust the sorting logic based on your needs
            return $"movieactor.{sorting}";
        }
    }
}
