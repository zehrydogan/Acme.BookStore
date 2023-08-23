using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Movies
{
    public class MovieDto : AuditedEntityDto<Guid>
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public MovieType Type { get; set; }

        public float IMDBRatings { get; set; }
    }
}