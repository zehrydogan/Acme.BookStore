using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.UserBooks
{
    public class CreateUpdateUserBooktDto
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

    }
}
