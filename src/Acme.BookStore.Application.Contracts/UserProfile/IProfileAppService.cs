using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.UserProfile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<List<BookDto>> GetRecommendedBooks();
        Task<List<MovieDto>> GetRecommendedMovies();
    }
}