using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.MovieComments
{
    public class MovieCommentDto : AuditedEntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }

    }
}


