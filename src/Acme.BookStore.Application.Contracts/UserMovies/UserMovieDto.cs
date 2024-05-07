using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.UserMovies
{
    public class UserMovieDto : AuditedEntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }
    }
}


