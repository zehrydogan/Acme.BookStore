using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.BookComments
{
    public class BookCommentDto : AuditedEntityDto<Guid>
    {
        public Guid BookId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public Guid UserId { get; set; }


    }

}
