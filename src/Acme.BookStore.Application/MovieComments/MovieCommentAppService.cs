using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.MovieComments
{
    public class MovieCommentAppService :
       CrudAppService<
           MovieComment, //The Book entity
           MovieCommentDto, //Used to show books
           Guid, //Primary key of the book entity
           CreateUpdateMovieCommentDto>, //Used to create/update a book
       IMovieCommentAppService //implement the IBookAppService
    {
        public MovieCommentAppService(IRepository<MovieComment, Guid> repository)
            : base(repository)
        {

        }

        public Task<MovieCommentDto> CreateAsync(CreateUpdateMovieCommentDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid MovieId, CreateUpdateMovieCommentDto input)
        {
            throw new NotImplementedException();
        }
    }
}