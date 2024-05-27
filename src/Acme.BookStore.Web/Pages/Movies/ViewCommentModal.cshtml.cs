using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.MovieComments;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Acme.BookStore.Web.Pages.Movies;

public class ViewCommentModalModel : BookStorePageModel
{
    private readonly IMovieCommentAppService _movieCommentAppService;
    private readonly IIdentityUserAppService _identityUserAppService;
    public List<CommentViewModel> Comments { get; set; }

    public ViewCommentModalModel(IMovieCommentAppService movieCommentAppService, IIdentityUserAppService identityUserAppService)
    {
        _movieCommentAppService = movieCommentAppService;
        _identityUserAppService = identityUserAppService;
    }

    public async Task OnGetAsync(Guid movieId)
    {
        var allComments = await _movieCommentAppService.GetListAsync(new PagedAndSortedResultRequestDto());
        var comments= allComments.Items.Where(b => b.MovieId == movieId).ToList();
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

 

    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }  
        
        public DateTime Date { get; set; }
        public int Rate { get; set; }

    }
}
