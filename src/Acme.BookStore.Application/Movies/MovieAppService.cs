﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Actors;
using Acme.BookStore.Directors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Movies
{
    [Authorize(BookStorePermissions.Movies.Default)]
    public class MovieAppService : CrudAppService<Movie, MovieDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieDto>, IMovieAppService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IRepository<MovieActor, Guid> _movieActorRepository;
        private readonly IDirectorRepository _directorRepository;

        public MovieAppService(
            IRepository<Movie, Guid> repository,
            IRepository<MovieActor, Guid> movieActorRepository,
            IActorRepository actorRepository,
            IDirectorRepository directorRepository)
            : base(repository)
        {
            _directorRepository = directorRepository;
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
            var queryableDirector = await _directorRepository.GetQueryableAsync();


            var query = from movie in queryable
                        where movie.Id == id
                        select movie;
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }

            var queryDirector = from movie in queryable
                                join director in await _directorRepository.GetQueryableAsync() on movie.DirectorId equals director.Id
                                where movie.Id == id
                                select new { movie, director };
            var queryDirectorResult = await AsyncExecuter.FirstOrDefaultAsync(queryDirector);
            if (queryDirectorResult == null)
            {
                throw new EntityNotFoundException(typeof(Movie), id);
            }
            var movieActorQuery = from movieactor in queryableMovieActor
                                  where movieactor.MovieId == id
                                  select movieactor;

            var movieActorQueryResult = await AsyncExecuter.ToListAsync(movieActorQuery);


            var actorQuery = from actor in queryableActor
                             where movieActorQueryResult.Select(ma => ma.ActorId).Contains(actor.Id)
                             select actor;

            var actorQueryResult = await AsyncExecuter.ToListAsync(actorQuery);

            var movieDto = ObjectMapper.Map<Movie, MovieDto>(queryDirectorResult.movie);
            movieDto.DirectorName = queryDirectorResult.director.Name;

            var actorDtoList = actorQueryResult.Select(ObjectMapper.Map<Actor, ActorDto>).ToList();
            movieDto.Actors = actorDtoList;


            return movieDto;
        }

        public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();
            var queryableMovieActor = await _movieActorRepository.GetQueryableAsync();
            var queryableActor = await _actorRepository.GetQueryableAsync();
            var queryableDirector = await _directorRepository.GetQueryableAsync();


            var query = from movie in queryable
                        select movie;

            var queryDirector = from movie in queryable
                                join director in await _directorRepository.GetQueryableAsync() on movie.DirectorId equals director.Id
                                select new { movie, director };

            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);
            var queryDirectorResult = await AsyncExecuter.ToListAsync(queryDirector);

            var movieDtos = queryDirectorResult.Select(x =>
            {
                var movieDto = ObjectMapper.Map<Movie, MovieDto>(x.movie);
                movieDto.DirectorName = x.director.Name;

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

        public override async Task<MovieDto> CreateAsync(CreateUpdateMovieDto input)
        {
    //        double.TryParse(s, System.Globalization.NumberStyles.Float,
    //System.Globalization.CultureInfo.InvariantCulture, out f)


            var movie = await base.CreateAsync(input);

            var movieActors = input.Actors.Select(actorId => new MovieActor
            {
                MovieId = movie.Id,
                ActorId = actorId
            }).ToList();

            await _movieActorRepository.InsertManyAsync(movieActors);

            return movie;
        }


        public override async Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input)
        {
            var movie = await base.UpdateAsync(id, input);


            var existingMovieActors = await _movieActorRepository.GetListAsync(ma => ma.MovieId == id);
            var existingActorId = existingMovieActors.Select(ma => ma.ActorId).ToList();
            var newActorId = input.Actors;

            var actorsToAdd = newActorId.Except(existingActorId).ToList();
            var actorsToRemove = existingActorId.Except(newActorId).ToList();

            foreach (var actorId in actorsToAdd)
            {
                await _movieActorRepository.InsertAsync(new MovieActor
                {
                    MovieId = id,
                    ActorId = actorId
                });
            }
            foreach (var actorId in actorsToRemove)
            {
                var movieActorToDelete = existingMovieActors.FirstOrDefault(ma => ma.ActorId == actorId);
                if (movieActorToDelete != null)
                {
                    await _movieActorRepository.DeleteAsync(movieActorToDelete);
                }
            }

            movie.DirectorId = input.DirectorId;


            return movie;
        }

        public async Task<ListResultDto<ActorLookupDto>> GetActorLookupAsync()
        {
            var actors = await _actorRepository.GetListAsync();

            return new ListResultDto<ActorLookupDto>(
                ObjectMapper.Map<List<Actor>, List<ActorLookupDto>>(actors)
            );
        }
        public async Task<ListResultDto<DirectorLookupDto>> GetDirectorLookupAsync()
        {
            var directors = await _directorRepository.GetListAsync();

            return new ListResultDto<DirectorLookupDto>(
                ObjectMapper.Map<List<Director>, List<DirectorLookupDto>>(directors)
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