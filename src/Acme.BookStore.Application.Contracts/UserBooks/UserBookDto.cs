using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.UserBooks
{
    public class UserBookDto : AuditedEntityDto<Guid>
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
    }
}


