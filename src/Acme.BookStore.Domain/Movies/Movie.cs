﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Movies;

public class Movie : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public MovieType Type { get; set; }

    public float IMDBRatings { get; set; }
    public Guid DirectorId { get; set; }
    public byte[] ImageContent { get; set; }


}
