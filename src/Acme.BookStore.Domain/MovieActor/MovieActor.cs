using Acme.BookStore.Actors;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Movies
{
    public class MovieActor : AggregateRoot<Guid>
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }

    }

   
}