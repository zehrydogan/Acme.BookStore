using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.MovieComments
{
    public class CreateUpdateMovieCommentDto
    {

        [Required]
        [StringLength(MovieCommentConsts.MaxNameLength)]
        public string Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
        public int Rate { get; set; }

    }
}
