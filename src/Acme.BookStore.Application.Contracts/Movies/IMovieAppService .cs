using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Movies;

public interface IMovieAppService :
    ICrudAppService< //Defines CRUD methods
        MovieDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateMovieDto> //Used to create/update a book
{
    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();

}



