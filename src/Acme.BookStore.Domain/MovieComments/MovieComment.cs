using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.MovieComments
{
    public class MovieComment : AggregateRoot<Guid>
    {
        public Guid MovieId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public MovieComment(string comment, DateTime date , Guid movieId)
        {
            Comment = comment;
            Date = date;
            MovieId = movieId;
        }
        public int Rate { get; set; }
        public Guid UserId { get; set; }
    }
}
