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

    public async Task OnGetAsync(Guid bookId)
    {
        BookComment = new BookCommentViewModel();
        BookComment.BookId = bookId;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var bookComment = new CreateUpdateBookCommentDto
        {
            Date = DateTime.Now,
            BookId = BookComment.BookId,
            Comment = BookComment.Comment
        };
        await _bookCommentAppService.CreateAsync(bookComment);
        return NoContent();
    }

    public class BookCommentViewModel
    {
        [HiddenInput]
        public Guid BookId {get;set;}

        [Required]
        [StringLength(BookCommentConsts.MaxNameLength)]
        public string Comment { get; set; }
    }
}