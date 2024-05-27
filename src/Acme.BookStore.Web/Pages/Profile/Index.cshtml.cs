using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using Acme.BookStore.UserBooks;
using Acme.BookStore.UserMovies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Acme.BookStore.Web.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserBookAppService _userBookAppService;
        private readonly IUserMovieAppService _userMovieAppService;
        private readonly IMovieAppService _movieAppService;
        private readonly IBookAppService _bookAppService;
        public readonly ICurrentUser _currentUser;
        public List<Movie> Movies { get; set; } 
        public List<Book> Books { get; set; }

        public IndexModel(IUserBookAppService userBookAppService, IUserMovieAppService userMovieAppService, ICurrentUser currentUser, IMovieAppService movieAppService, IBookAppService bookAppService)
        {
            _userBookAppService = userBookAppService;
            _userMovieAppService = userMovieAppService;
            _currentUser = currentUser;
            _movieAppService = movieAppService;
            _bookAppService = bookAppService;
            Movies = new List<Movie>();
            Books = new List<Book>();
        }
        public async Task OnGet()
        {
            var allUserBooks = await _userBookAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var userBooks = allUserBooks.Items.Where(b => b.UserId == _currentUser.Id).ToList();
            var books = new List<Book>();

            var allUserMovies = await _userMovieAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var userMovies = allUserMovies.Items.Where(b => b.UserId == _currentUser.Id).ToList();
            var movies = new List<Movie>();

            foreach (var userBook in userBooks)
            {
                var book = await _bookAppService.GetAsync(userBook.BookId);
                Books.Add(new Book { ImageContent = book.ImageContent, Name = book.Name });
            }

            foreach (var userMovie in userMovies)
            {
                var movie = await _movieAppService.GetAsync(userMovie.MovieId);
                Movies.Add(new Movie { ImageContent = movie.ImageContent, Name = movie.Name });
            }
        }
    }

    public class Book
    {
        public string Name { get; set; }
        public byte[] ImageContent { get; set; }
    }

    public class Movie
    {
        public string Name { get; set; }
        public byte[] ImageContent { get; set; }
    }
}
