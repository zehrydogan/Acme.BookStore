using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Movies;


public class ActorLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
