using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.BookComments
{
    public class CreateUpdateBookCommentDto
    {

        [Required]
        [StringLength(BookCommentConsts.MaxNameLength)]
        public string Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Guid BookId { get; set; }

        public int Rate { get; set; }

    }
}
