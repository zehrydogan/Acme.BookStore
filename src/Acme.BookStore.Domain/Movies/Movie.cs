﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Movies;

public class Movie : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public string Director { get; set; }

    public MovieType Type { get; set; }

    public float IMDBRatings { get; set; }
}

    //public Guid ActorId { get; set; }


