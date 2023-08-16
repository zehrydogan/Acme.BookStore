using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Movies;

public class MovieAppService :
    CrudAppService<
        Movie, //The Book entity
        MovieDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMovieDto>, //Used to create/update a book
    IMovieAppService //implement the IBookAppService
{
    public MovieAppService(IRepository<Movie, Guid> repository)
        : base(repository)
    {
        GetPolicyName = Permissions.BookStorePermissions.Movies.Default;
        GetListPolicyName = Permissions.BookStorePermissions.Movies.Default;
        CreatePolicyName = Permissions.BookStorePermissions.Movies.Create;
        UpdatePolicyName = Permissions.BookStorePermissions.Movies.Edit;
        DeletePolicyName = Permissions.BookStorePermissions.Movies.Delete;
    }
}
