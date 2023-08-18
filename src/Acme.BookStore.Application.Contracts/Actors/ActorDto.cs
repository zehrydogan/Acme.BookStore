using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Actors;

public class ActorDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Gender { get; set; }


    public DateTime BirthDate { get; set; }

}
