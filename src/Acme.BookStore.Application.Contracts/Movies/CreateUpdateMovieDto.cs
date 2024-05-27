using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Acme.BookStore.Movies
{
    public class CreateUpdateMovieDto



    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid MovieId { get; set; }

        public Guid ActorId { get; set; }
        public Guid DirectorId { get; set; }

        public List<Guid> Actors { get; set; }

        [Required]
        public MovieType Type { get; set; }

        [Range(0, 10, ErrorMessage = "IMDb rating 0 ile 10 arasında olmalıdır.")]
        [Required]

        public double? IMDBRatings { get; set; }
        public byte[] ImageContent { get; set; }
    }
}