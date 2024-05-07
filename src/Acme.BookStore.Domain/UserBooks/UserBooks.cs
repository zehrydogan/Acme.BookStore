using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.UserBooks
{
    public class UserBook : AggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }
      
    }
}
