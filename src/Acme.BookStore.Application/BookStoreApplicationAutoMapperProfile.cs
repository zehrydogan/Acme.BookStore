using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using Acme.BookStore.Actors;
using Acme.BookStore.Directors;
using AutoMapper;
using Acme.BookStore.BookComments;
using Acme.BookStore.MovieComments;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
//        CreateMap<Book, Books.AuthorLookupDto>();


        CreateMap<Author, AuthorDto>();
        CreateMap<CreateUpdateAuthorDto, Author>();
        CreateMap<Author, AuthorLookupDto>();

        CreateMap<Movie, MovieDto>();
        CreateMap<CreateUpdateMovieDto, Movie>();

        CreateMap<Actor, ActorDto>();
        CreateMap<CreateUpdateActorDto, Actor>();
        CreateMap<Actor, ActorLookupDto>();

        CreateMap<Director, DirectorDto>();
        CreateMap<CreateUpdateDirectorDto, Director>();
        CreateMap<Director, DirectorLookupDto>();

        CreateMap<BookComment, BookCommentDto>();
        CreateMap<MovieComment, MovieCommentDto>();

        CreateMap<CreateUpdateBookCommentDto, BookComment>();
        CreateMap<CreateUpdateMovieCommentDto, MovieComment>();




    }
}