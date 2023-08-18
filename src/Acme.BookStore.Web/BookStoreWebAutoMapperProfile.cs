using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Authors;
using Acme.BookStore.Movies;

using AutoMapper;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()

    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<MovieDto, CreateUpdateMovieDto>();


        CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel, CreateUpdateAuthorDto>();
        CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
        CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,  UpdateAuthorDto>();

        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();

        CreateMap<Pages.Movies.CreateModalModel.CreateMovieViewModel, CreateUpdateMovieDto>();
        CreateMap<MovieDto, Pages.Movies.EditModalModel.EditMovieViewModel>();
        CreateMap<Pages.Movies.EditModalModel.EditMovieViewModel, CreateUpdateMovieDto>();


    }
}

