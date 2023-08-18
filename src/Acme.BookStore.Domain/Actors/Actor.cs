﻿using System;
using JetBrains.Annotations;

using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Actors;

public class Actor : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    public string Gender { get; private set; }

    public DateTime BirthDate { get; set; }

    private Actor()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Actor(
        Guid id,
        [NotNull] string name,
        [NotNull] string gender,


        [NotNull] DateTime birthDate)
        : base(id)
    {
        SetName(name);
        BirthDate = birthDate;
        
    }

    internal Actor ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: ActorConsts.MaxNameLength
        );
    }

}
