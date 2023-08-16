using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Movies
{
    public class MovieDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Director { get; set; }

        public MovieType Type { get; set; }

        public float IMDBRatings { get; set; }

        public Guid DirectorId { get; set; }

    }
}




