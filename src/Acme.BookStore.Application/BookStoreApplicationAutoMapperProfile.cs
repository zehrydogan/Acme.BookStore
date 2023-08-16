using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Movies;

using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, Books.AuthorLookupDto>();
        CreateMap<Movie, MovieDto>();
        CreateMap<CreateUpdateMovieDto, Movie>();
        //CreateMap<Author, Movies.AuthorLookupDto>();





    }
}