using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.UserMovies
{
    public class UserMovie : AggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

    }
}
