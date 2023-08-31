using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Directors;

public class DirectorDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Gender { get; set; }


    public DateTime BirthDate { get; set; }

}
