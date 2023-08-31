using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Actors;
using Acme.BookStore.Movies;
using Acme.BookStore.Directors;


using AutoMapper;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()

    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<MovieDto, CreateUpdateMovieDto>();
        CreateMap<ActorDto, CreateUpdateActorDto>();
        CreateMap<AuthorDto, CreateUpdateAuthorDto>();



        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel, CreateUpdateAuthorDto>();
        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel, CreateUpdateAuthorDto>();

        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();

        CreateMap<Pages.Movies.CreateModalModel.CreateMovieViewModel, CreateUpdateMovieDto>();
        CreateMap<MovieDto, Pages.Movies.EditModalModel.EditMovieViewModel>();
        CreateMap<Pages.Movies.EditModalModel.EditMovieViewModel, CreateUpdateMovieDto>();

        CreateMap<Pages.Actors.CreateModalModel.CreateActorViewModel, CreateUpdateActorDto>();
        CreateMap<ActorDto, Pages.Actors.EditModalModel.EditActorViewModel>();
        CreateMap<Pages.Actors.EditModalModel.EditActorViewModel, CreateUpdateActorDto>();

        CreateMap<Pages.Directors.CreateModalModel.CreateDirectorViewModel, CreateUpdateDirectorDto>();
        CreateMap<DirectorDto, Pages.Directors.EditModalModel.EditDirectorViewModel>();
        CreateMap<Pages.Directors.EditModalModel.EditDirectorViewModel, CreateUpdateDirectorDto>();



    }
}
