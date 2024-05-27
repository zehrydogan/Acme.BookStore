using Acme.BookStore.MovieComments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Acme.BookStore.Web.Pages.MovieComments;

public class MovieCommentModalModel : BookStorePageModel
{
    [BindProperty]
    public MovieCommentViewModel MovieComment { get; set; }

    private readonly IMovieCommentAppService _movieCommentAppService;
        private readonly CurrentUser _currentUser;

    public MovieCommentModalModel(IMovieCommentAppService movieCommentAppService, CurrentUser currentUser)
    {
        _movieCommentAppService = movieCommentAppService;
        _currentUser = currentUser;
    }

    public async Task OnGetAsync(Guid movieId)
    {
        MovieComment = new MovieCommentViewModel();
        MovieComment.MovieId = movieId;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var movieComment = new CreateUpdateMovieCommentDto
        {
            Date = DateTime.Now,
            MovieId = MovieComment.MovieId,
            Comment = MovieComment.Comment,
            Rate = MovieComment.Rate,
            UserId = _currentUser.Id.Value

        };
        await _movieCommentAppService.CreateAsync(movieComment);
        return NoContent();
    }

    public class MovieCommentViewModel
    {

        [HiddenInput]
        public Guid MovieId { get; set; }

        [Required]
        [StringLength(MovieCommentConsts.MaxNameLength)]

        public string Comment { get; set; }
        public int Rate { get; set; }




    }
}
