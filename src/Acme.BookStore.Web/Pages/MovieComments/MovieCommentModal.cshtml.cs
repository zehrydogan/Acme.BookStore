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

    public async Task OnGetAsync(Guid id)
    {
        var movieCommentDto = await _movieCommentAppService.GetAsync(id);
        MovieComment = ObjectMapper.Map<MovieCommentDto, MovieCommentViewModel>(movieCommentDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _movieCommentAppService.UpdateAsync(
            MovieComment.Id,
            ObjectMapper.Map<MovieCommentViewModel, CreateUpdateMovieCommentDto>(MovieComment)
        );

        return NoContent();
    }

    public class MovieCommentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [StringLength(MovieCommentConsts.MaxNameLength)]
        public string Comment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;


    }
}
