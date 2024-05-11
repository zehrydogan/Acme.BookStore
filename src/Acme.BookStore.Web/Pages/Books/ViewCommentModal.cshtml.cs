using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.BookComments;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Acme.BookStore.Web.Pages.Books;

public class ViewCommentModalModel : BookStorePageModel
{
    private readonly IBookCommentAppService _bookCommentAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    public List<CommentViewModel> Comments { get; set; }

    public ViewCommentModalModel(IBookCommentAppService bookCommentAppService, IIdentityUserAppService identityUserAppService)
    {
        _bookCommentAppService = bookCommentAppService;
        _identityUserAppService = identityUserAppService;
    }

    public async Task OnGetAsync(Guid bookId)
    {
        var allComments = await _bookCommentAppService.GetListAsync(new PagedAndSortedResultRequestDto());
        var comments= allComments.Items.Where(b => b.BookId == bookId).ToList();
        var commentModels = new List<CommentViewModel>();
        foreach (var comment in comments)
        {
            var user = await _identityUserAppService.GetAsync(comment.UserId);

            if (user == null) continue;

            var commentModel = new CommentViewModel
            {
                Comment = comment.Comment,
                UserName = user.Name,
                Date = comment.Date,
                Id = comment.Id,
                Rate = comment.Rate
            };
            commentModels.Add(commentModel);
        }
        Comments = commentModels;
    }

    public async Task<IActionResult> OnPostAsync()
    {
       
        return NoContent();
    }

    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }  
        
        public DateTime Date { get; set; }
        public int Rate { get; set; }

    }
}
