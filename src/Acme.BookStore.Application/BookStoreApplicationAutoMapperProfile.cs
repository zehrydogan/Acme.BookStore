using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Movies;
using Acme.BookStore.Actors;
using Acme.BookStore.Directors;



using AutoMapper;

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





    }
}