using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Books;

public class CreateModalModel : BookStorePageModel
{
    [BindProperty]
    public CreateBookViewModel Book { get; set; }

    public List<SelectListItem> Authors { get; set; }

    private readonly IBookAppService _bookAppService;

    public CreateModalModel(
        IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    public async Task OnGetAsync()
    {
        Book = new CreateBookViewModel();

        var authorLookup = await _bookAppService.GetAuthorLookupAsync();
        Authors = authorLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var book = ObjectMapper.Map<CreateBookViewModel, CreateUpdateBookDto>(Book);
        if (Book.File != null)
        {
            using (var ms = new MemoryStream())
            {
                Book.File.CopyTo(ms);
                book.ImageContent = ms.ToArray();
            }
        }
        await _bookAppService.CreateAsync(book);
        return NoContent();
    }

    public class CreateBookViewModel
    {
        [SelectItems(nameof(Authors))]
        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Adventure;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }
    }
}
