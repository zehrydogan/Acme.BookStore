using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Actors;

public class GetActorListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
