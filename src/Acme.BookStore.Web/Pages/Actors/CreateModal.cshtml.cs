using Acme.BookStore.Actors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Actors;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateActorViewModel Actor { get; set; }

    private readonly IActorAppService _actorAppService;

    public CreateModalModel(IActorAppService actorAppService)
    {
        _actorAppService = actorAppService;
    }

    public void OnGet()
    {
        Actor = new CreateActorViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateActorViewModel, CreateUpdateActorDto>(Actor);
        await _actorAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateActorViewModel
    {
        [Required]
        [StringLength(ActorConsts.MaxNameLength)]
        public string Name { get; set; }


        [Required]
        public GenderType Gender { get; set; } = GenderType.Male;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        
    }
}
