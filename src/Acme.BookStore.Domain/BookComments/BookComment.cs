using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.BookComments
{
    public class BookComment : AggregateRoot<Guid>
    {
        public Guid BookId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public BookComment(string comment, DateTime date, Guid bookId)
        {
            Comment = comment;
            Date = date;
            BookId = bookId;
        }

    }
}
