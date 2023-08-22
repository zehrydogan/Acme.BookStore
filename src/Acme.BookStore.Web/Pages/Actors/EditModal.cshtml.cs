using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Actors;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Actors;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditActorViewModel Actor { get; set; }

    private readonly IActorAppService _actorAppService;

    public EditModalModel(IActorAppService actorAppService)
    {
        _actorAppService = actorAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var actorDto = await _actorAppService.GetAsync(id);
        Actor = ObjectMapper.Map<ActorDto, EditActorViewModel>(actorDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _actorAppService.UpdateAsync(
            Actor.Id,
            ObjectMapper.Map<EditActorViewModel, CreateUpdateActorDto>(Actor)
        );

        return NoContent();
    }

    public class EditActorViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

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
