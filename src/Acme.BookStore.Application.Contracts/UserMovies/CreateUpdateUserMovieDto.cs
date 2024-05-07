using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.UserMovies
{
    public class CreateUpdateUserMovietDto
    {
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }

    }
}
