﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Movies
{
    public class CreateUpdateMovieDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public MovieType Type { get; set; }

        [Required]
        public float IMDBRatings { get; set; }


     
    }
}
