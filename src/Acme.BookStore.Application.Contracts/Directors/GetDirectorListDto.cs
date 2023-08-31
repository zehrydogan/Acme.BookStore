using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Directors;

public class GetDirectorListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
