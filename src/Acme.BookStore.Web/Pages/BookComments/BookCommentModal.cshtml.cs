using Acme.BookStore.BookComments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.BookComments;

public class BookCommentModalModel : BookStorePageModel
{
    [BindProperty]
    public BookCommentViewModel BookComment { get; set; }

    private readonly IBookCommentAppService _bookCommentAppService;

    public BookCommentModalModel(IBookCommentAppService bookCommentAppService)
    {
        _bookCommentAppService = bookCommentAppService;
    }

    public async Task OnGetAsync(Guid id)
    {
        var bookCommentDto = await _bookCommentAppService.GetAsync(id);
        BookComment = ObjectMapper.Map<BookCommentDto, BookCommentViewModel>(bookCommentDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _bookCommentAppService.UpdateAsync(
            BookComment.Id,
            ObjectMapper.Map<BookCommentViewModel, CreateUpdateBookCommentDto>(BookComment)
        );

        return NoContent();
    }

    public class BookCommentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(BookCommentConsts.MaxNameLength)]
        public string Comment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;


    }
}
