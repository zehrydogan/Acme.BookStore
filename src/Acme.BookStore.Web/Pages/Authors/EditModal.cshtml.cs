using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Authors;

public class EditModalModel : BookStorePageModel
{
    [BindProperty]
    public EditAuthorViewModel Author { get; set; }

    private readonly IAuthorAppService _authorAppService;

    public EditModalModel(IAuthorAppService authorAppService)
    {
        _authorAppService = authorAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var authorDto = await _authorAppService.GetAsync(id);
        Author = ObjectMapper.Map<AuthorDto, EditAuthorViewModel>(authorDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _authorAppService.UpdateAsync(
            Author.Id,
            ObjectMapper.Map<EditAuthorViewModel, CreateUpdateAuthorDto>(Author)
        );

        return NoContent();
    }

    public class EditAuthorViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [TextArea]
        [Required]
        public string ShortBio { get; set; }
    }
}
