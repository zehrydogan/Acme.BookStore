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
using Volo.Abp.FeatureManagement.JsonConverters;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.Movies
{
    [Authorize(BookStorePermissions.Movies.Default)]
    public class MovieAppService : CrudAppService<Movie, MovieDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieDto>, IMovieAppService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IRepository<MovieActor, Guid> _movieActorRepository;

        public MovieAppService(
            IRepository<Movie, Guid> repository,
            IRepository<MovieActor, Guid> movieActorRepository,
            IActorRepository actorRepository)
            : base(repository)
        {
            _actorRepository = actorRepository;
            _movieActorRepository = movieActorRepository;
            GetPolicyName = BookStorePermissions.Movies.Default;
            GetListPolicyName = BookStorePermissions.Movies.Default;
            CreatePolicyName = BookStorePermissions.Movies.Create;
            UpdatePolicyName = BookStorePermissions.Movies.Edit;
            DeletePolicyName = BookStorePermissions.Movies.Delete;
        }

        public override async Task<MovieDto> GetAsync(Guid id)
        {
            var queryable = await Repository.GetQueryableAsync();
            var queryableMovieActor = await _movieActorRepository.GetQueryableAsync();
            var queryableActor = await _actorRepository.GetQueryableAsync();

            var query = from movie in queryable
                        where movie.Id == id
                        select movie;

            var movieActorQuery = from movieactor in queryableMovieActor
                                  where movieactor.MovieId == id
                                  select movieactor;

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            var movieActorQueryResult = await AsyncExecuter.ToListAsync(movieActorQuery);


            var actorQuery = from actor in queryableActor
                             where movieActorQueryResult.Select(ma => ma.ActorId).Contains(actor.Id)
                             select actor;

            var actorQueryResult = await AsyncExecuter.ToListAsync(actorQuery);

            var movieDto = ObjectMapper.Map<Movie, MovieDto>(queryResult);
            var actorDtoList = actorQueryResult.Select(ObjectMapper.Map<Actor, ActorDto>).ToList();
            movieDto.Actors = actorDtoList;


            return movieDto;
        }

        public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();
            var queryableMovieActor = await _movieActorRepository.GetQueryableAsync();
            var queryableActor = await _actorRepository.GetQueryableAsync();


            var query = from movie in queryable
                        select movie;

            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var movieDtos = queryResult.Select(x =>
            {
                var movieDto = ObjectMapper.Map<Movie, MovieDto>(x);
                return movieDto;
            }).ToList();

            foreach (var movieDto in movieDtos)
            {

                var movieActorQuery = from movieactor in queryableMovieActor
                                      where movieactor.MovieId == movieDto.Id
                                      select movieactor;
                var movieActorQueryResult = await AsyncExecuter.ToListAsync(movieActorQuery);

                var actorQuery = from actor in queryableActor
                                 where movieActorQueryResult.Select(ma => ma.ActorId).Contains(actor.Id)
                                 select actor;

                var actorQueryResult = await AsyncExecuter.ToListAsync(actorQuery);

                movieDto.Actors = actorQueryResult.Select(ObjectMapper.Map<Actor, ActorDto>).ToList();
            }

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
                return $"{nameof(Movie.Name)}";
            }

            return $"{sorting}";
        }
    }
}
