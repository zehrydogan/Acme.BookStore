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
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from movieactor in queryable
                        join actor in await _actorRepository.GetQueryableAsync() on movieactor.ActorId equals actor.Id
                        where movieactor.Id == id
                        select new { movieactor, actor };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(MovieActor), id);
            }


            var movieDto = ObjectMapper.Map<MovieActor, MovieDto>(queryResult.movieactor);
            return movieDto;
        }

        public override async Task<PagedResultDto<MovieDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();

            var query = from movie in queryable
                        select movie;

            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var movieDtos = queryResult.Select(x =>
            {
                var movieDto = ObjectMapper.Map<MovieActor, MovieDto>(x);
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
                return $"{nameof(Movie.Name)}";
            }

            return $"{sorting}";
        }
    }
}
