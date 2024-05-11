using Acme.BookStore.BookComments;
using Acme.BookStore.MovieComments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Acme.BookStore.Web.Pages.BookComments;

public class BookCommentModalModel : BookStorePageModel
{
    [BindProperty]
    public BookCommentViewModel BookComment { get; set; }

    private readonly IBookCommentAppService _bookCommentAppService;
    private readonly CurrentUser _currentUser;


    public BookCommentModalModel(IBookCommentAppService bookCommentAppService, CurrentUser currentUser)
    {
        _bookCommentAppService = bookCommentAppService;
        _currentUser = currentUser;
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
            Comment = BookComment.Comment,
            Rate = BookComment.Rate,
            UserId = _currentUser.Id.Value
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
        public int Rate { get; set; }

    }
}