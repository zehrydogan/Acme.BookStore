using Acme.BookStore.Actors;
using Acme.BookStore.Directors;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.Diagnostics;

namespace Acme.BookStore.Movies
{
    public class MovieDto : AuditedEntityDto<Guid>
    {
        public List<ActorDto> Actors { get; set; }
        public string DirectorName { get; set; }

        public Guid DirectorId { get; set; }

        public string Name { get; set; }

        public MovieType Type { get; set; }

        
        public float IMDBRatings { get; set; }
        public byte[] ImageContent { get; set; }

    }
}