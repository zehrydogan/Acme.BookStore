using Acme.BookStore.Directors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Directors;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateDirectorViewModel Director { get; set; }

    private readonly IDirectorAppService _directorAppService;

    public CreateModalModel(IDirectorAppService directorAppService)
    {
        _directorAppService = directorAppService;
    }

    public void OnGet()
    {
        Director = new CreateDirectorViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateDirectorViewModel, CreateUpdateDirectorDto>(Director);
        await _directorAppService.CreateAsync(dto);
        return NoContent();
    }

    public class CreateDirectorViewModel
    {
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
