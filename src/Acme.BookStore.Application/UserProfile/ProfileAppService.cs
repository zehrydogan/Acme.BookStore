using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using Acme.BookStore.UserBooks;
using Acme.BookStore.UserMovies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.UserProfile
{
    public class ProfileAppService : ApplicationService, IProfileAppService
    {
        private IRepository<Movie, Guid> MovieRepository { get; set; }
        private IRepository<Book, Guid> BookRepository { get; set; }
        private IRepository<UserMovie, Guid> UserMovieRepository { get; set; }
        private IRepository<UserBook, Guid> UserBookRepository { get; set; }


        public ProfileAppService(IRepository<Movie, Guid> movieRepository, IRepository<Book, Guid> bookRepository, 
            IRepository<UserMovie, Guid> userMovieRepository, IRepository<UserBook, Guid> userBookRepository)
        {
            MovieRepository = movieRepository;
            BookRepository = bookRepository;
            UserMovieRepository = userMovieRepository;
            UserBookRepository = userBookRepository;
        }

        public async Task<List<BookDto>> GetRecommendedBooks()
        {
            var recommendedBooks = new List<BookDto>();
            var userBookList = await UserBookRepository.GetListAsync(m => m.UserId == CurrentUser.Id);
            var mostReadAuthorsByUser = new Dictionary<Guid, int>();

            foreach (var userBook in userBookList)
            {
                var book = await BookRepository.GetAsync(userBook.BookId);

                if (mostReadAuthorsByUser.ContainsKey(book.AuthorId))
                    mostReadAuthorsByUser[book.AuthorId]++;
                else
                    mostReadAuthorsByUser.Add(book.AuthorId, 1);
            }

            var orderedMostReadAuthorsByUser = mostReadAuthorsByUser.OrderByDescending(c => c.Value).Select(c => c).ToList();


            var userBookIds = userBookList.Select(book => book.BookId).ToList();
            foreach(var author in orderedMostReadAuthorsByUser)
            {
                if (recommendedBooks.Count == 5) break;
                
                var authorBooks = await BookRepository.GetListAsync(b => !userBookIds.Contains(b.Id) && b.AuthorId == author.Key);

                foreach(var book in authorBooks)
                {
                    if (recommendedBooks.Count < 5)
                        recommendedBooks.Add(ObjectMapper.Map<Book, BookDto>(book));
                    else
                        break;
                }
            }

            if(recommendedBooks.Count < 5)
            {
                var mostReadBooks = await GetMostReadBooks();

                foreach(var book in mostReadBooks.Where(b => !userBookIds.Contains(b.Id)))
                {
                    if (recommendedBooks.Count < 5 && !recommendedBooks.Any(b => b.Id == book.Id))
                        recommendedBooks.Add(book);
                    else
                        break;
                }
            }

            return recommendedBooks;
        }

        public async Task<List<MovieDto>> GetRecommendedMovies()
        {
            var recommendedMovies = new List<MovieDto>();
            var userMovieList = await UserMovieRepository.GetListAsync(m => m.UserId == CurrentUser.Id);

            var mostWatchedDirectorByUser = new Dictionary<Guid, int>();
            var mostWatchedCategoriesByUser = new Dictionary<MovieType, int>();

            foreach (var userMovie in userMovieList)
            {
                var movie = await MovieRepository.GetAsync(userMovie.MovieId);

                if (mostWatchedDirectorByUser.ContainsKey(movie.DirectorId))
                    mostWatchedDirectorByUser[movie.DirectorId]++;
                else
                    mostWatchedDirectorByUser.Add(movie.DirectorId, 1);

                if (mostWatchedCategoriesByUser.ContainsKey(movie.Type))
                    mostWatchedCategoriesByUser[movie.Type]++;
                else
                    mostWatchedCategoriesByUser.Add(movie.Type, 1);
            }

            var orderedMostWatchedDirectorsByUser = mostWatchedDirectorByUser.OrderByDescending(c => c.Value).Select(c => c).ToList();
            var orderedMostWatchedCategoriesByUser = mostWatchedCategoriesByUser.OrderByDescending(c => c.Value).Select(c => c).ToList();
            var userMovieIds = userMovieList.Select(movie => movie.MovieId).ToList();
            
            foreach (var category in orderedMostWatchedCategoriesByUser)
            {
                if (recommendedMovies.Count == 3) break;

                var categoryMovies = await MovieRepository.GetListAsync(b => !userMovieIds.Contains(b.Id) && b.Type == category.Key);

                foreach (var movie in categoryMovies.Where(b => !userMovieIds.Contains(b.Id)))
                {
                    if (recommendedMovies.Count < 3)
                        recommendedMovies.Add(ObjectMapper.Map<Movie, MovieDto>(movie));
                }
            }

            foreach (var category in orderedMostWatchedDirectorsByUser)
            {
                if (recommendedMovies.Count == 5) break;

                var directorMovies = await MovieRepository.GetListAsync(b => !userMovieIds.Contains(b.Id) && b.DirectorId == category.Key);

                foreach (var movie in directorMovies.Where(b => !userMovieIds.Contains(b.Id)))
                {
                    if (recommendedMovies.Count < 5 && !recommendedMovies.Any(m => m.Id == movie.Id))
                        recommendedMovies.Add(ObjectMapper.Map<Movie, MovieDto>(movie));
                }
            }

            if (recommendedMovies.Count < 5)
            {
                var mostWatchedMovies = await GetMostWatchedMovies();

                foreach (var movie in mostWatchedMovies.Where(b => !userMovieIds.Contains(b.Id)))
                {
                    if (recommendedMovies.Count < 5 && !recommendedMovies.Any(b => b.Id == movie.Id))
                        recommendedMovies.Add(movie);
                }
            }

            return recommendedMovies;
        }

        private async Task<List<MovieDto>> GetMostWatchedMovies()
        {
            var mostWatchedMovies = new List<MovieDto>();
            var userMovieList = await UserMovieRepository.GetListAsync();
            var movieIds = userMovieList.GroupBy(m => m.MovieId).OrderByDescending(g => g.Count()).Select(x => x.Key).ToList();

            foreach(var movieId in movieIds) 
            {
                var movie = await MovieRepository.GetAsync(movieId);
                mostWatchedMovies.Add(ObjectMapper.Map<Movie, MovieDto>(movie));
            }

            return mostWatchedMovies;
        }

        private async Task<List<BookDto>> GetMostReadBooks()
        {
            var mostReadBooks = new List<BookDto>();
            var userBookList = await UserBookRepository.GetListAsync();
            var bookIds = userBookList.GroupBy(m => m.BookId).OrderByDescending(g => g.Count()).Select(x => x.Key).ToList();

            foreach (var bookId in bookIds)
            {
                var book = await BookRepository.GetAsync(bookId);
                mostReadBooks.Add(ObjectMapper.Map<Book, BookDto>(book));
            }

            return mostReadBooks;
        }
    }
}
