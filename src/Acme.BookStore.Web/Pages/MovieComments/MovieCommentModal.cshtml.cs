using Acme.BookStore.MovieComments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.MovieComments;

public class MovieCommentModalModel : BookStorePageModel
{
    [BindProperty]
    public MovieCommentViewModel MovieComment { get; set; }

    private readonly IMovieCommentAppService _movieCommentAppService;

    public MovieCommentModalModel(IMovieCommentAppService movieCommentAppService)
    {
        _movieCommentAppService = movieCommentAppService;
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
            Rate = MovieComment.Rate
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
