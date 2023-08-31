using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Directors;

public class Director : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    public GenderType Gender { get; set; }

    public DateTime BirthDate { get; set; }



    private Director()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Director(
        Guid id,
        [NotNull] string name,
        [NotNull] DateTime birthDate,
        [NotNull] GenderType gender

        )

        : base(id)
    {
        SetName(name);
        BirthDate = birthDate;
        Gender = gender;

    }

    internal Director ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: DirectorConsts.MaxNameLength
        );
    }

}
