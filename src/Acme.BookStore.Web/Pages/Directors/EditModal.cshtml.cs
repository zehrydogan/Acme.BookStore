using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Directors;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Directors;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditDirectorViewModel Director { get; set; }

    private readonly IDirectorAppService _directorAppService;

    public EditModalModel(IDirectorAppService directorAppService)
    {
        _directorAppService = directorAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var directorDto = await _directorAppService.GetAsync(id);
        Director = ObjectMapper.Map<DirectorDto, EditDirectorViewModel>(directorDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _directorAppService.UpdateAsync(
            Director.Id,
            ObjectMapper.Map<EditDirectorViewModel, CreateUpdateDirectorDto>(Director)
        );

        return NoContent();
    }

    public class EditDirectorViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(DirectorConsts.MaxNameLength)]
        public string Name { get; set; }


        [Required]
        public GenderType Gender { get; set; } = GenderType.Male;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;


    }
}
