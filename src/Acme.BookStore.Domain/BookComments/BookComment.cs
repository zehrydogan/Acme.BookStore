using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.BookComments
{
    public class BookComment : AggregateRoot<Guid>
    {
        public Guid BookId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public int Rate { get; set; }

        public BookComment(string comment, DateTime date, Guid bookId, Guid userId)
        {
            Comment = comment;
            Date = date;
            BookId = bookId;
            UserId = userId;
        }
    }
}
